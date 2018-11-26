using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;
using System.Data.SqlClient;
using System.Configuration;

namespace Server.DataAccessLayer {
    public class OrderDB : ICRUD<Order>, IOrder {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public OrderDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public OrderDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public int CreateReturnID(Order Entity, bool test = false, bool testResult = false) {
            int insertedID = -1;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {


                    cmd.CommandText = "Insert into [dbo].[Order](total, purchaseTime, customerID) OUTPUT INSERTED.OrderID values (@Total, @PurchaseTime, @CustomerID)";
                    cmd.Parameters.AddWithValue("Total", Entity.Total);
                    cmd.Parameters.AddWithValue("PurchaseTime", Entity.DateCreated);
                    cmd.Parameters.AddWithValue("CustomerID", Entity.Customer.ID);
                    insertedID = (int)cmd.ExecuteScalar();

                    foreach (OrderLine ol in Entity.Orderlines) {
                        cmd.CommandText = "INSERT INTO Orderline (Quantity, SubTotal, OrderID, ProductID) Values " +
                                                    "(@Quantity, @SubTotal, @OrderID, @ProductID)";
                        cmd.Parameters.AddWithValue("Quantity", ol.Quantity);
                        cmd.Parameters.AddWithValue("SubTotal", ol.SubTotal);
                        cmd.Parameters.AddWithValue("OrderID", insertedID);
                        cmd.Parameters.AddWithValue("ProductID", ol.Product.ID);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    //catch (SqlException) {

                    //}
                }
            }
            return insertedID;
        }

        public bool Delete(Order Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }

        public Order Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll() {
            throw new NotImplementedException();
        }

        public bool Update(Order Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }

        public bool Create(Order Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }
    }
}
