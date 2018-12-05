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
    public class OrderDB : IOrder {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public OrderDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public OrderDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public Order Create(Order Entity, bool test = false, bool testResult = false) {
            Order order = new Order();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {

                        cmd.CommandText = "Insert into [dbo].[Order](total, purchaseTime, customerID) OUTPUT INSERTED.OrderID values (@Total, @PurchaseTime, @CustomerID)";
                        cmd.Parameters.AddWithValue("Total", Entity.Total);
                        cmd.Parameters.AddWithValue("PurchaseTime", Entity.DateCreated);
                        cmd.Parameters.AddWithValue("CustomerID", Entity.Customer.ID);
                        order.ID = (int)cmd.ExecuteScalar();

                        if(Entity.Orderlines.Count > 0) {
                            foreach (OrderLine ol in Entity.Orderlines) {
                                cmd.CommandText = "INSERT INTO Orderline (Quantity, SubTotal, OrderID, ProductID) Values " +
                                                            "(@Quantity, @SubTotal, @OrderID, @ProductID)";
                                cmd.Parameters.AddWithValue("Quantity", ol.Quantity);
                                cmd.Parameters.AddWithValue("SubTotal", ol.SubTotal);
                                cmd.Parameters.AddWithValue("OrderID", order.ID);
                                cmd.Parameters.AddWithValue("ProductID", ol.Product.ID);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        else {
                            order.ErrorMessage = "Ordren har ingen ordrelinjer";
                        }
                    }
                }
                catch (SqlException e) {
                    order.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return order;
        }

        public Order Delete(Order Entity, bool test = false, bool testResult = false) {
            Order order = new Order();
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction()) {
                            byte[] rowID = null;
                            int rowCount = 0;
                            using (SqlCommand cmd = connection.CreateCommand()) {
                                cmd.Transaction = transaction;
                                cmd.CommandText = "SELECT rowID from [dbo].[order] WHERE orderID = @OrderID";
                                cmd.Parameters.AddWithValue("orderID", Entity.ID);

                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read()) {
                                    rowID = (byte[])reader["rowID"];
                                }
                                reader.Close();

                                cmd.CommandText = "DELETE from [dbo].[Order] WHERE OrderID = @OrderID AND rowID = @rowID";
                                cmd.Parameters.AddWithValue("rowID", rowID);
                                rowCount = cmd.ExecuteNonQuery();

                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }

                                if (rowCount == 0) {
                                    order.ErrorMessage = "Ordren blev ikke slettet. Prøv igen.";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    order.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException e) {
                        order.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }
            }
            return order;
        }

        public Order Get(int id) {
            Order o = new Order();
            Customer c = new Customer();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        
                        cmd.CommandText = "SELECT orderID, total, purchaseTime, customerID from [dbo].[Order] where orderID = @orderID";
                        cmd.Parameters.AddWithValue("orderID", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            o.ID = reader.GetInt32(reader.GetOrdinal("orderID"));
                            o.Total = reader.GetDecimal(reader.GetOrdinal("total"));
                            o.DateCreated = reader.GetDateTime(reader.GetOrdinal("purchaseTime"));
                            if (!reader.IsDBNull(reader.GetOrdinal("customerID"))) {
                                c.ID = reader.GetInt32(reader.GetOrdinal("customerID"));
                            }
                            else {
                                c.ID = 0;
                            }
                        }
                        reader.Close();
                        cmd.Parameters.Clear();

                        if (c.ID > 0) {
                            cmd.CommandText = "SELECT customerID, email from Customer where customerID = @customerID";
                            cmd.Parameters.AddWithValue("customerID", c.ID);
                            SqlDataReader customerReader = cmd.ExecuteReader();
                            while (customerReader.Read()) {
                                c.Email = customerReader.GetString(customerReader.GetOrdinal("email"));
                            }
                            customerReader.Close();
                            cmd.Parameters.Clear();

                            o.Customer = c;
                        }
                        else {
                            c.ErrorMessage = "Der blev ikke fundet en kunde til ordren";
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
                    }
                }
                catch (SqlException e) {
                    o.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return o;
        }

        public IEnumerable<Order> GetAll() {
            throw new NotImplementedException();
        }

        public Order Update(Order Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }
    }
}
