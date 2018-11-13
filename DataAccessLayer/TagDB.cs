using System;
using System.Collections.Generic;
using System.Configuration;
using Server.Domain;
using System.Data.SqlClient;

namespace Server.DataAccessLayer
{
    public class TagDB
    {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public TagDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public TagDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public Tag Get(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    Tag t = new Tag();

                    cmd.CommandText = "SELECT tagID From Tag Where Name = @Name";
                    cmd.Parameters.AddWithValue("Name", name);
                    SqlDataReader tagIDReader = cmd.ExecuteReader();
                    while (tagIDReader.Read())
                    {
                        
                        t.TagID = tagIDReader.GetInt32(tagIDReader.GetOrdinal("tagID"));

                    }
                    tagIDReader.Close();

                    cmd.CommandText = "Select productID From ProductTag Where tagID = @tagID";
                    cmd.Parameters.AddWithValue("tagID", t.TagID);
                    SqlDataReader productReader = cmd.ExecuteReader();
                    while (productReader.Read())
                    {
                        Product p = new Product();
                        p.ID = productReader.GetInt32(productReader.GetOrdinal("productID"));
                        t.Products.Add(p);
                    }
                    productReader.Close();
                    return t;
                }
            }
        }
    }
}

