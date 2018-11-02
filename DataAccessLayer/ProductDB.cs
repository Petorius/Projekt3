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

        public void Delete(int id) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                int rowId = 0;
                int rowCount;

                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT rowID FROM product WHERE productID = @productID";
                    cmd.Parameters.AddWithValue("productID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read()) {
                        rowId = reader.GetInt32(reader.GetOrdinal("rowID"));
                    }

                    cmd.CommandText = "DELETE FROM Product WHERE ProductID = @productID AND rowID = @rowId";
                    cmd.Parameters.AddWithValue("productID", id);
                    cmd.Parameters.AddWithValue("rowID", rowId);
                    rowCount = cmd.ExecuteNonQuery();

                    if(rowCount == 0) {
                        cmd.Transaction.Rollback();
                    }
                }
            }
        }

        public Product Get(int id) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT id, name, price, stock, description, rating from Product";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Product p = new Product();
                        p.ID = reader.GetInt32(reader.GetOrdinal("id"));
                        p.Name = reader.GetString(reader.GetOrdinal("name"));
                        p.Price = reader.GetDouble(reader.GetOrdinal("price"));
                        p.Stock = reader.GetInt32(reader.GetOrdinal("stock"));
                        p.Description = reader.GetString(reader.GetOrdinal("description"));
                        p.Rating = reader.GetInt32(reader.GetOrdinal("rating"));

                        if (id == p.ID) {
                            return p;
                        }
                    }
                    return null;
                }
            }
        }

        public void Update(int ID) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    int rowID = 0;
                    cmd.CommandText = "SELECT RowID FROM Product WHERE ProductID = @ID";
                    cmd.Parameters.AddWithValue("ID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        rowID = reader.GetInt32(reader.GetOrdinal("RowID"));
                    }
                    cmd.CommandText = "UPDATE Product WHERE ProductID = @ID AND RowID = @RowID";
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.Parameters.AddWithValue("RowID", rowID);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public IEnumerable<Product> GetAll() {
            throw new NotImplementedException();
        }

    }
}
