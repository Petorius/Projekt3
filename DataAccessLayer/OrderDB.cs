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
            bool res = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "DELETE from [dbo].[Order] WHERE OrderID = @OrderID";
                    cmd.Parameters.AddWithValue("OrderID", Entity.ID);
                    cmd.ExecuteNonQuery();
                    res = true;

                    //Mangler at tælle stock og quantity op
                }
            }
            return res;
        }

        public Order Get(int id) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    Order o = new Order();
                    int customerID = -1;
                    Customer c = new Customer();
                    //build order
                    cmd.CommandText = "SELECT orderID, total, purchaseTime, customerID from [dbo].[Order] where orderID = @orderID";
                    cmd.Parameters.AddWithValue("orderID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        o.ID = reader.GetInt32(reader.GetOrdinal("orderID"));
                        o.Total = reader.GetDecimal(reader.GetOrdinal("total"));
                        o.DateCreated = reader.GetDateTime(reader.GetOrdinal("purchaseTime"));
                        customerID = reader.GetInt32(reader.GetOrdinal("customerID"));
                    }
                    reader.Close();
                    cmd.Parameters.Clear();

                    if(customerID > 0) {
                        cmd.CommandText = "SELECT customerID, email from Customer where customerID = @customerID";
                        cmd.Parameters.AddWithValue("customerID", customerID);
                        SqlDataReader customerReader = cmd.ExecuteReader();
                        while (customerReader.Read()) {
                            c.Email = customerReader.GetString(customerReader.GetOrdinal("email"));
                        }
                        customerReader.Close();
                        cmd.Parameters.Clear();

                        o.Customer = c;
                    }
                    else {

                        c.Email = "deleted user";
                        o.Customer = c;
                    }
                    

                    //build orderlines
                    cmd.CommandText = "Select orderlineID, quantity, subTotal, orderID, productID from Orderline where Orderline.orderID = @orderID";
                    cmd.Parameters.AddWithValue("orderID", o.ID);
                    SqlDataReader orderLineReader = cmd.ExecuteReader();
                    while (orderLineReader.Read()) {
                        OrderLine ol = new OrderLine();
                        ol.ID = orderLineReader.GetInt32(orderLineReader.GetOrdinal("orderlineID"));
                        ol.Quantity = orderLineReader.GetInt32(orderLineReader.GetOrdinal("quantity"));
                        ol.SubTotal = orderLineReader.GetDecimal(orderLineReader.GetOrdinal("subtotal"));
                        Product p = new Product();
                        p.ID = orderLineReader.GetInt32(orderLineReader.GetOrdinal("productID"));
                        ol.Product = p;

                        o.Orderlines.Add(ol);
                    }
                    orderLineReader.Close();

                    if (id == o.ID) {
                        return o;
                    }
                }
                return null;
            }
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
