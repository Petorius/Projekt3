using System;
using System.Collections.Generic;
using System.Configuration;
using Server.Domain;
using System.Data.SqlClient;
using DataAccessLayer.Interface;

namespace Server.DataAccessLayer {
    public class ProductDB : IProduct {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public ProductDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public ProductDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public Product Create(Product Entity, bool test = false, bool testResult = false) {
            int insertedID = -1;
            Product p = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "Insert into Product(Name, Price, Stock, MinStock, MaxStock, Description, Sales, IsActive) OUTPUT INSERTED.ProductID values" +
                                " (@Name, @Price, @Stock, @MinStock, @MaxStock, @Description, @Sales, @IsActive)";
                        cmd.Parameters.AddWithValue("Name", Entity.Name);
                        cmd.Parameters.AddWithValue("Price", Entity.Price);
                        cmd.Parameters.AddWithValue("Stock", Entity.Stock);
                        cmd.Parameters.AddWithValue("MinStock", Entity.MinStock);
                        cmd.Parameters.AddWithValue("MaxStock", Entity.MaxStock);
                        cmd.Parameters.AddWithValue("Description", Entity.Description);
                        cmd.Parameters.AddWithValue("Sales", Entity.Sales);
                        cmd.Parameters.AddWithValue("IsActive", true);
                        insertedID = (int)cmd.ExecuteScalar();

                        foreach (Image i in Entity.Images) {
                            cmd.CommandText = "Insert into Image(ImageSource, Name, ProductID) values" +
                                " (@ImageSource, @ImageName, @ProductID)";
                            cmd.Parameters.AddWithValue("ImageSource", i.ImageSource);
                            cmd.Parameters.AddWithValue("ImageName", i.Name);
                            cmd.Parameters.AddWithValue("ProductID", insertedID);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                }
                catch (SqlException e) {
                    p.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return p;
        }

        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
        public Product Delete(Product Entity, bool test = false, bool testResult = false) {
            Product p = new Product();
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
                        connection.Open();
                        using (SqlTransaction trans = connection.BeginTransaction()) {
                            byte[] rowId = null;
                            int rowCount = 0;
                            using (SqlCommand cmd = connection.CreateCommand()) {
                                cmd.Transaction = trans;
                                cmd.CommandText = "SELECT rowID FROM product WHERE productID = @productID";
                                cmd.Parameters.AddWithValue("productID", Entity.ID);
                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read()) {
                                    rowId = (byte[])reader["rowId"];
                                }
                                reader.Close();

                                cmd.CommandText = "UPDATE Product set isActive = 0 where ProductID = @productID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }

                                if (rowCount == 0) {
                                    p.ErrorMessage = "Produktet blev ikke slettet. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    p.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException e) {
                        p.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }
            }
                return p;
        }
        
        public Product Get(string select, string input) {
            Product p = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "SELECT productid, name, price, stock, description, rating, minstock, maxstock, sales, isActive from Product where " + select + " = @" + select;
                        cmd.Parameters.AddWithValue(select, input);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            p.ID = reader.GetInt32(reader.GetOrdinal("productid"));
                            p.Name = reader.GetString(reader.GetOrdinal("name"));
                            p.Price = reader.GetDecimal(reader.GetOrdinal("price"));
                            p.Stock = reader.GetInt32(reader.GetOrdinal("stock"));
                            p.MinStock = reader.GetInt32(reader.GetOrdinal("minstock"));
                            p.MaxStock = reader.GetInt32(reader.GetOrdinal("maxstock"));
                            p.Description = reader.GetString(reader.GetOrdinal("description"));
                            p.Sales = reader.GetInt32(reader.GetOrdinal("sales"));
                            p.IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"));
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                        
                    }
                    if (p.ID < 1) {
                        p.ErrorMessage = "Produktet findes ikke";
                    }
                }

                catch (SqlException e) {
                    p.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return p;
        }

        public List<Image> GetProductImages(int id) {
            List<Image> images = new List<Image>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "Select ImageSource, Name from Image where Image.ProductID = @productID";
                        cmd.Parameters.AddWithValue("productID", id);
                        SqlDataReader imageReader = cmd.ExecuteReader();
                        while (imageReader.Read()) {
                            Image i = new Image();
                            i.ImageSource = imageReader.GetString(imageReader.GetOrdinal("ImageSource"));
                            i.Name = imageReader.GetString(imageReader.GetOrdinal("Name"));

                            images.Add(i);
                        }
                        imageReader.Close();
                        cmd.Parameters.Clear();

                    }
                }
                catch (SqlException) {
                    return null;
                }
            }
            return images;
        }

        public Product Update(Product p, bool test = false, bool testResult = false) {
            Product product = new Product();
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
                        connection.Open();
                        using (SqlTransaction trans = connection.BeginTransaction()) {
                            byte[] rowId = null;
                            int rowCount = 0;
                            using (SqlCommand cmd = connection.CreateCommand()) {
                                cmd.Transaction = trans;
                                cmd.CommandText = "SELECT rowID FROM product WHERE productID = @productID";
                                cmd.Parameters.AddWithValue("productID", p.ID);
                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read()) {
                                    rowId = (byte[])reader["rowId"];
                                }
                                reader.Close();

                                cmd.CommandText = "UPDATE Product " +
                                    "SET name = @name, price = @price, stock = @stock, minStock = @minStock, maxStock = @maxStock, description = @description, isActive = @isActive " +
                                    "WHERE productID = @productID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("name", p.Name);
                                cmd.Parameters.AddWithValue("price", p.Price);
                                cmd.Parameters.AddWithValue("stock", p.Stock);
                                cmd.Parameters.AddWithValue("minStock", p.MinStock);
                                cmd.Parameters.AddWithValue("maxStock", p.MaxStock);
                                cmd.Parameters.AddWithValue("description", p.Description);
                                cmd.Parameters.AddWithValue("isActive", p.IsActive);
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }

                                if (rowCount == 0) {
                                    product.ErrorMessage = "Produktet blev ikke opdateret. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    product.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException e) {
                        product.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }

            }
            return product;
        }

        public IEnumerable<Product> GetAll() {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    
                    using (SqlCommand cmd = connection.CreateCommand()) {

                        cmd.CommandText = "SELECT * from Product where isActive = 1";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            Product p = new Product();
                            p.ID = reader.GetInt32(reader.GetOrdinal("productid"));
                            p.Name = reader.GetString(reader.GetOrdinal("name"));
                            p.Price = reader.GetDecimal(reader.GetOrdinal("price"));
                            p.Stock = reader.GetInt32(reader.GetOrdinal("stock"));
                            p.MinStock = reader.GetInt32(reader.GetOrdinal("minstock"));
                            p.MaxStock = reader.GetInt32(reader.GetOrdinal("maxstock"));
                            p.Description = reader.GetString(reader.GetOrdinal("description"));
                            p.IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"));
                            p.Sales = reader.GetInt32(reader.GetOrdinal("sales"));

                            products.Add(p);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                    
                }
                catch (SqlException) {
                    return null;
                }
            }
            return products;
        }

        public Product Get(int id) {
            throw new NotImplementedException();
        }
    }
}
