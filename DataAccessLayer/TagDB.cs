using System;
using System.Collections.Generic;
using System.Configuration;
using Server.Domain;
using System.Data.SqlClient;

namespace Server.DataAccessLayer {
    public class TagDB {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public TagDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public TagDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public Tag Get(string name) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                Tag t = new Tag();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    

                    cmd.CommandText = "SELECT tagID From Tag Where Name = @Name";
                    cmd.Parameters.AddWithValue("Name", name);
                    SqlDataReader tagIDReader = cmd.ExecuteReader();
                    while (tagIDReader.Read()) {

                        t.TagID = tagIDReader.GetInt32(tagIDReader.GetOrdinal("tagID"));

                    }
                    tagIDReader.Close();

                    cmd.CommandText = "Select productID From ProductTag Where tagID = @tagID";
                    cmd.Parameters.AddWithValue("tagID", t.TagID);
                    SqlDataReader productReader = cmd.ExecuteReader();
                    while (productReader.Read()) {
                        Product p = new Product();
                        p.ID = productReader.GetInt32(productReader.GetOrdinal("productID"));
                        t.Products.Add(p);
                    }
                    productReader.Close();
                }


                using (SqlCommand cmd2 = connection.CreateCommand()) {

                    foreach (Product p in t.Products) {
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
                return t;
            }
        }
    }
}

