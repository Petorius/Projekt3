using System;
using System.Collections.Generic;
using System.Configuration;
using Domain;
using System.Transactions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    public class ProductDB : ICRUD<Product> {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        public ProductDB() {

        }

        public void Create(Product Entity) {
            using (TransactionScope scope = new TransactionScope()) {
                using(SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    int id = 0;
                    int row = 0;
                    using(SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "Insert into Product(Name, Price, Stock, MinStock, MaxStock, Description, Rating) values" +
                            " (@Name, @Price, @Stock, @MinStock, @MaxStock, @Description, @Rating)";
                        cmd.Parameters.AddWithValue("Name", Entity.Name);
                        cmd.Parameters.AddWithValue("Price", Entity.Price);
                        cmd.Parameters.AddWithValue("Stock", Entity.Stock);
                        cmd.Parameters.AddWithValue("MinStock", Entity.MinStock);
                        cmd.Parameters.AddWithValue("MaxStock", Entity.MaxStock);
                        cmd.Parameters.AddWithValue("Description", Entity.Description);
                        cmd.Parameters.AddWithValue("Rating", Entity.Rating);
                        id = (int)cmd.ExecuteScalar();
                    }

                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "Select rowid from Product where productID = @id";
                        cmd.Parameters.AddWithValue("id", id);
                        row = (int)cmd.ExecuteScalar();
                    }
                    

                    Console.WriteLine(row);

                }
                scope.Complete();
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
