using Server.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DataAccessLayer;
using System.Data.SqlClient;

namespace Server.DataAccessLayer
{
    public class SalesDB
    {
        private string connectionString;
        private CategoryDB categoryDB;
        private ProductDB productDB;

        public SalesDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            categoryDB = new CategoryDB();
            productDB = new ProductDB();
        }

        public Category GetSalesByCategory(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Category c = categoryDB.Get(name);
                return c;
            }
        }

        public IEnumerable<Product> GetAllSales()
        {
            return productDB.GetAll();
        }

        public Tag GetSalesByTag(string name)
        {
            throw new NotImplementedException();
            //Do this later, maybe
        }


    }

}
