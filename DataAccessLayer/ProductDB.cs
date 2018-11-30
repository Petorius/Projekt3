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

        public bool Create(Product Entity, bool test = false, bool testResult = false) {
            int insertedID = -1;
            bool isCompleted = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
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
                if (insertedID > 0) {
                    isCompleted = true;
                }
            }
            return isCompleted;
        }

        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
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
                            cmd.CommandText = "UPDATE Product set isActive = 0 where ProductID = @productID AND rowID = @rowId";
                            cmd.Parameters.AddWithValue("rowID", rowId);
                            rowCount = cmd.ExecuteNonQuery();

                            // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
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

                    cmd.CommandText = "SELECT productid, name, price, stock, description, rating, minstock, maxstock, sales, isActive from Product where productID = @ProductID";
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
                        p.Sales = reader.GetInt32(reader.GetOrdinal("sales"));
                        p.IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"));
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
                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT Text, DateCreated, UserID from Review where Review.ProductID = @productID";
                    cmd.Parameters.AddWithValue("productID", p.ID);
                    SqlDataReader reviewReader = cmd.ExecuteReader();
                    while(reviewReader.Read()) {
                        Review r = new Review();
                        r.Text = reviewReader.GetString(reviewReader.GetOrdinal("Text"));
                        r.DateCreated = reviewReader.GetDateTime(reviewReader.GetOrdinal("DateCreated"));
                        r.User.ID = reviewReader.GetInt32(reviewReader.GetOrdinal("UserID"));

                        p.Reviews.Add(r);
                    }
                    reviewReader.Close();
                    cmd.Parameters.Clear();

                    foreach (Review review in p.Reviews) {
                        cmd.CommandText = "SELECT FirstName FROM Customer WHERE CustomerID = @UserID";
                        cmd.Parameters.AddWithValue("UserID", review.User.ID);

                        SqlDataReader userReader = cmd.ExecuteReader();
                        while(userReader.Read()) {
                            review.User.FirstName = userReader.GetString(userReader.GetOrdinal("FirstName"));
                        }
                        userReader.Close();
                        cmd.Parameters.Clear();
                    }
                    return p;
                }
                
            }
        }

        public Product GetByName(string name) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    Product p = new Product();

                    cmd.CommandText = "SELECT productid, name, price, stock, description, rating, minstock, maxstock, sales, isActive from Product where name = @Name";
                    cmd.Parameters.AddWithValue("name", name);
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

                    if (name == p.Name) {
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
