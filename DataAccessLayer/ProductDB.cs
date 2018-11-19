using System;
using System.Collections.Generic;
using System.Configuration;
using Server.Domain;
using System.Data.SqlClient;


namespace Server.DataAccessLayer {
    public class ProductDB : ICRUD<Product> {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public ProductDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public ProductDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public bool Create(Product Entity, bool test = false, bool testResult = false) {
            bool isCompleted = true;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    try {
                        cmd.CommandText = "Insert into Product(Name, Price, Stock, MinStock, MaxStock, Description) values" +
                                " (@Name, @Price, @Stock, @MinStock, @MaxStock, @Description)";
                        cmd.Parameters.AddWithValue("Name", Entity.Name);
                        cmd.Parameters.AddWithValue("Price", Entity.Price);
                        cmd.Parameters.AddWithValue("Stock", Entity.Stock);
                        cmd.Parameters.AddWithValue("MinStock", Entity.MinStock);
                        cmd.Parameters.AddWithValue("MaxStock", Entity.MaxStock);
                        cmd.Parameters.AddWithValue("Description", Entity.Description);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException) {
                        isCompleted = false;
                    }
                }
            }
            return isCompleted;
        }

        public bool Delete(Product Entity, bool test = false, bool testResult = false) {
            bool deleted = false;
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
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

                            //try {
                            cmd.CommandText = "DELETE FROM Product WHERE ProductID = @productID AND rowID = @rowId";
                            cmd.Parameters.AddWithValue("rowID", rowId);
                            rowCount = cmd.ExecuteNonQuery();

                            if (test) {
                                rowCount = testResult ? 1 : 0;
                            }

                            if (rowCount == 0) {
                                cmd.Transaction.Rollback();
                            }
                            else {
                                deleted = true;
                                cmd.Transaction.Commit();
                                break;
                            }
                        }
                        //catch (SqlException) {
                        //    deleted = false;
                        //}

                    }


                }

            }
            return deleted;
        }

        public Product Get(int id) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    Product p = new Product();

                    cmd.CommandText = "SELECT productid, name, price, stock, description, rating, minstock, maxstock from Product where productID = @ProductID";
                    cmd.Parameters.AddWithValue("ProductID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        p.ID = reader.GetInt32(reader.GetOrdinal("productid"));
                        p.Name = reader.GetString(reader.GetOrdinal("name"));
                        p.Price = reader.GetDecimal(reader.GetOrdinal("price"));
                        p.Stock = reader.GetInt32(reader.GetOrdinal("stock"));
                        p.MinStock = reader.GetInt32(reader.GetOrdinal("minstock"));
                        p.MaxStock = reader.GetInt32(reader.GetOrdinal("maxstock"));
                        p.Description = reader.GetString(reader.GetOrdinal("description"));
                        //p.Rating = reader.GetInt32(reader.GetOrdinal("rating"));
                    }
                    reader.Close();
                    cmd.Parameters.Clear();

                    

                    cmd.CommandText = "Select ImageSource, Name from Image where Image.ProductID = @productID";
                    cmd.Parameters.AddWithValue("productID", p.ID);
                    SqlDataReader imageReader = cmd.ExecuteReader();
                    while (imageReader.Read()) {
                        Image i = new Image();
                        i.ImageSource = imageReader.GetString(imageReader.GetOrdinal("ImageSource"));
                        i.Name = imageReader.GetString(imageReader.GetOrdinal("Name"));

                        p.Images.Add(i);
                    }
                    imageReader.Close();

                    if (id == p.ID) {
                        return p;
                    }
                }
                return null;
            }
        }

        public bool Update(Product p, bool test = false, bool testResult = false) {
            bool isUpdated = false;
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
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

                            //try {
                            cmd.CommandText = "UPDATE Product " +
                                "SET name = @name, price = @price, stock = @stock, minStock = @minStock, maxStock = @maxStock, description = @description " +
                                "WHERE productID = @productID AND rowID = @rowId";
                            cmd.Parameters.AddWithValue("name", p.Name);
                            cmd.Parameters.AddWithValue("price", p.Price);
                            cmd.Parameters.AddWithValue("stock", p.Stock);
                            cmd.Parameters.AddWithValue("minStock", p.MinStock);
                            cmd.Parameters.AddWithValue("maxStock", p.MaxStock);
                            cmd.Parameters.AddWithValue("description", p.Description);
                            cmd.Parameters.AddWithValue("rowID", rowId);
                            rowCount = cmd.ExecuteNonQuery();

                            if (test) {
                                rowCount = testResult ? 1 : 0;
                            }

                            if (rowCount == 0) {
                                cmd.Transaction.Rollback();
                            }
                            else {
                                isUpdated = true;
                                cmd.Transaction.Commit();
                                break;
                            }
                            //}
                            //catch (SqlException) {
                            //    isUpdated = false;
                            //}

                        }
                    }
                }
            }
            return isUpdated;
        }

        public IEnumerable<Product> GetAll() {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                List<Product> products = new List<Product>();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    

                    cmd.CommandText = "SELECT * from Product";
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
                        //p.Rating = reader.GetInt32(reader.GetOrdinal("rating"));

                        products.Add(p);
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd2 = connection.CreateCommand()) {

                    foreach (Product p in products) {
                        cmd2.CommandText = "Select ImageSource, Name from Image where Image.ProductID = @productID";
                        cmd2.Parameters.AddWithValue("productID", p.ID);
                        SqlDataReader imageReader = cmd2.ExecuteReader();
                        while (imageReader.Read()) {
                            Image i = new Image();
                            i.ImageSource = imageReader.GetString(imageReader.GetOrdinal("ImageSource"));
                            i.Name = imageReader.GetString(imageReader.GetOrdinal("Name"));

                            p.Images.Add(i);
                        }
                        imageReader.Close();
                        cmd2.Parameters.Clear();

                    }
                }
                return products;
            }
        }
    }
}
