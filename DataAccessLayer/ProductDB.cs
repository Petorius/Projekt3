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
                        cmd.ExecuteNonQuery();
                    }
                }
        }

        public bool Delete(int id) {
            bool deleted = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                byte[] rowId = null;
                int rowCount = 0;

                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT rowID FROM product WHERE productID = @productID";
                    cmd.Parameters.AddWithValue("productID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read()) {
                        rowId = (byte[])reader["rowId"];
                    }
                    reader.Close();

                    try {
                        cmd.CommandText = "DELETE FROM Product WHERE ProductID = @productID AND rowID = @rowId";
                        cmd.Parameters.AddWithValue("rowID", rowId);
                        rowCount = cmd.ExecuteNonQuery();
                        if (rowCount == 0) {
                            cmd.Transaction.Rollback();
                        }
                        else {
                            deleted = true;
                        }
                    } catch (SqlException) {
                        deleted = false;
                    }



                }
            }
            return deleted;
        }

        public Product Get(int id) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT productid, name, price, stock, description, rating, minstock, maxstock from Product where productID = @ProductID";
                    cmd.Parameters.AddWithValue("ProductID", id);
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

                        if (id == p.ID) {
                            return p;
                        }
                    }
                    return null;
                }
            }
        }

        public bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description) {
            bool isUpdated = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                byte[] rowId = null;
                int rowCount = 0;

                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT rowID FROM product WHERE productID = @productID";
                    cmd.Parameters.AddWithValue("productID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) {
                        rowId = (byte[])reader["rowId"];
                    }
                    reader.Close();

                    //try {
                        cmd.CommandText = "UPDATE Product " +
                            "SET name = @name, price = @price, stock = @stock, minStock = @minStock, maxStock = @maxStock, description = @description " +
                            "WHERE productID = @productID AND rowID = @rowId";
                        cmd.Parameters.AddWithValue("name", name);
                        cmd.Parameters.AddWithValue("price", price);
                        cmd.Parameters.AddWithValue("stock", stock);
                        cmd.Parameters.AddWithValue("minStock", minStock);
                        cmd.Parameters.AddWithValue("maxStock", maxStock);
                        cmd.Parameters.AddWithValue("description", description);
                        cmd.Parameters.AddWithValue("rowID", rowId);

                        rowCount = cmd.ExecuteNonQuery();
                        if (rowCount == 0) {
                            cmd.Transaction.Rollback();
                        }
                        else {
                            isUpdated = true;
                        }
                    //}
                    //catch (SqlException) {
                    //    isUpdated = false;
                    //}
                }
            }
            return isUpdated;
        }

        public IEnumerable<Product> GetAll() {
            throw new NotImplementedException();
        }

    }
}
