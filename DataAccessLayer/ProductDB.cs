using System;
using System.Collections.Generic;
using System.Configuration;
using Server.Domain;
using System.Transactions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public class ProductDB : ICRUD<Product> {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        public ProductDB() {

        }

        public void Create(Product Entity) {
                using(SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    using(SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "Insert into Product(Name, Price, Stock, MinStock, MaxStock, Description) values" +
                            " (@Name, @Price, @Stock, @MinStock, @MaxStock, @Description)";
                        cmd.Parameters.AddWithValue("Name", Entity.Name);
                        cmd.Parameters.AddWithValue("Price", Entity.Price);
                        cmd.Parameters.AddWithValue("Stock", Entity.Stock);
                        cmd.Parameters.AddWithValue("MinStock", Entity.MinStock);
                        cmd.Parameters.AddWithValue("MaxStock", Entity.MaxStock);
                        cmd.Parameters.AddWithValue("Description", Entity.Description);
                    }
                }
        }

        public void Delete(Product Entity) {
            throw new NotImplementedException();
        }

        public Product Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Product Enitity) {
            throw new NotImplementedException();
        }
    }
}
