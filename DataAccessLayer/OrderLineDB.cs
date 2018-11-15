using Server.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public class OrderLineDB : ICRUD<OrderLine> {
        private string connectionString;

        public OrderLineDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public OrderLineDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public bool Create(OrderLine Entity, bool test = false, bool testResult = false) {
            bool isCompleted = true;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction()) {
                    byte[] rowId = null;
                    int rowCount = 0;


                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.Transaction = trans;
                        cmd.CommandText = "SELECT rowID FROM product WHERE productID = @productID";
                        cmd.Parameters.AddWithValue("productID", Entity.Product.ID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read()) {
                            rowId = (byte[])reader["rowId"];
                        }
                        reader.Close();
                        try {
                            cmd.CommandText = "UPDATE Product " +
                                "SET stock = @stock WHERE productID = @productID AND rowID = @rowId";
                            cmd.Parameters.AddWithValue("stock", Entity.Product.Stock - Entity.Quantity);
                            cmd.Parameters.AddWithValue("rowID", rowId);
                            rowCount = cmd.ExecuteNonQuery();

                            if (test) {
                                rowCount = testResult ? 1 : 0;
                            }
                            if (rowCount == 0) {
                                cmd.Transaction.Rollback();
                            }
                            else {
                                cmd.Transaction.Commit();
                            }
                        }
                        catch (SqlException) {
                            isCompleted = false;
                        }

                    }
                }
            }
            return isCompleted;
        }

        public bool Delete(int id, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }

        public OrderLine Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderLine> GetAll() {
            throw new NotImplementedException();
        }

        public bool Update(OrderLine Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }
    }
}
