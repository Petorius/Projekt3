using Server.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer
{
    public class CategoryDB 
    {

        private string connectionString;
        private ProductDB productDB;

        public CategoryDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            productDB = new ProductDB();
        }


        public bool Create(Category Entity, bool test = false, bool testResult = false)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Category Entity, bool test = false, bool testResult = false)
        {
            throw new NotImplementedException();
        }

        public Category Get(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Category c = new Category();
                using (SqlCommand cmd = connection.CreateCommand())
                {


                    cmd.CommandText = "SELECT CategoryID From Category Where Name = @Name";
                    cmd.Parameters.AddWithValue("Name", name);
                    SqlDataReader categoryIDReader = cmd.ExecuteReader();
                    while (categoryIDReader.Read())
                    {
                        c.Name = name;
                        c.ID = categoryIDReader.GetInt32(categoryIDReader.GetOrdinal("CategoryID"));
                    }
                    categoryIDReader.Close();

                    cmd.CommandText = "Select productID From ProductCategory Where CategoryID = @CategoryID";
                    cmd.Parameters.AddWithValue("CategoryID", c.ID);
                    SqlDataReader productReader = cmd.ExecuteReader();
                    while (productReader.Read())
                    {
                        int foundProductID;
                        foundProductID = productReader.GetInt32(productReader.GetOrdinal("productID"));

                        Product p = productDB.Get(foundProductID);

                        c.Products.Add(p);
                    }
                    productReader.Close();
                }
                return c;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Category Entity, bool test = false, bool testResult = false)
        {
            throw new NotImplementedException();
        }
    }
}
