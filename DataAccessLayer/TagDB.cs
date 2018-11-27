using System;
using System.Collections.Generic;
using System.Configuration;
using Server.Domain;
using System.Data.SqlClient;

namespace Server.DataAccessLayer {
    public class TagDB {
        private string connectionString;
        private ProductDB productDB;

        // Database test constructor. Only used for unit testing.
        public TagDB(string connectionString) {
            this.connectionString = connectionString;
            productDB = new ProductDB(connectionString);
        }

        public TagDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            productDB = new ProductDB();
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
                        t.Name = name;
                        t.ID = tagIDReader.GetInt32(tagIDReader.GetOrdinal("tagID"));
                    }
                    tagIDReader.Close();

                    cmd.CommandText = "Select productID From ProductTag Where tagID = @tagID";
                    cmd.Parameters.AddWithValue("tagID", t.ID);
                    SqlDataReader productReader = cmd.ExecuteReader();
                    while (productReader.Read()) {
                        int foundProductID;
                        foundProductID = productReader.GetInt32(productReader.GetOrdinal("productID"));

                        Product p = productDB.Get(foundProductID);

                        t.Products.Add(p);
                    }
                    productReader.Close();
                }
                return t;
            }
        }
    }
}

