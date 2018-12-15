using DataAccessLayer.Interface;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public class OrderLineDB : IOrderLine {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public OrderLineDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public OrderLineDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
        public OrderLine Create(OrderLine Entity, bool test = false, bool testResult = false) {
            OrderLine orderLine = new OrderLine();
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
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
                                cmd.CommandText = "UPDATE Product " +
                                    "SET stock = @stock, sales = @sales WHERE productID = @productID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("stock", Entity.Product.Stock - Entity.Quantity);
                                cmd.Parameters.AddWithValue("sales", Entity.Product.Sales + Entity.Quantity);
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }
                                if (rowCount == 0) {
                                    orderLine.ErrorMessage = "Orderlinjen blev ikke oprettet. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    orderLine.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException e) {
                        orderLine.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }
            }
            return orderLine;
        }


        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
        public OrderLine Delete(OrderLine Entity, bool test = false, bool testResult = false) {
            OrderLine orderLine = new OrderLine();
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
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
                                cmd.CommandText = "UPDATE Product " +
                                    "SET stock = @stock, sales = @sales WHERE productID = @productID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("stock", Entity.Product.Stock + Entity.Quantity);
                                cmd.Parameters.AddWithValue("sales", Entity.Product.Sales - Entity.Quantity);
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }
                                if (rowCount == 0) {
                                    orderLine.ErrorMessage = "Ordrelinjen blev ikke slettet. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    orderLine.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }


                    }
                    catch (SqlException e) {
                        orderLine.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }


            }
            return orderLine;
        }

        public OrderLine Get(int id) {
            OrderLine ol = new OrderLine();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {

                        cmd.CommandText = "Select orderlineID, quantity, subTotal, orderID, productID from Orderline where orderlineID = @orderlineID";
                        cmd.Parameters.AddWithValue("orderlineID", id);
                        SqlDataReader orderLineReader = cmd.ExecuteReader();
                        while (orderLineReader.Read()) {

                            ol.ID = orderLineReader.GetInt32(orderLineReader.GetOrdinal("orderlineID"));
                            ol.Quantity = orderLineReader.GetInt32(orderLineReader.GetOrdinal("quantity"));
                            ol.SubTotal = orderLineReader.GetDecimal(orderLineReader.GetOrdinal("subtotal"));
                            Product p = new Product();
                            p.ID = orderLineReader.GetInt32(orderLineReader.GetOrdinal("productID"));
                            ol.Product = p;
                        }
                        orderLineReader.Close();


                    }

                }
                catch (SqlException e) {
                    ol.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return ol;
        }

        public IEnumerable<OrderLine> GetAll() {
            throw new NotImplementedException();
        }

        public List<OrderLine> GetOrderlinesByOrderID(int ID) {
            List<OrderLine> orderlines = new List<OrderLine>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "Select orderlineID, quantity, subTotal, orderID, productID from Orderline where Orderline.orderID = @orderID";
                        cmd.Parameters.AddWithValue("orderID", ID);
                        SqlDataReader orderLineReader = cmd.ExecuteReader();
                        while (orderLineReader.Read()) {
                            OrderLine ol = new OrderLine();
                            ol.ID = orderLineReader.GetInt32(orderLineReader.GetOrdinal("orderlineID"));
                            ol.Quantity = orderLineReader.GetInt32(orderLineReader.GetOrdinal("quantity"));
                            ol.SubTotal = orderLineReader.GetDecimal(orderLineReader.GetOrdinal("subtotal"));
                            Product p = new Product();
                            p.ID = orderLineReader.GetInt32(orderLineReader.GetOrdinal("productID"));
                            ol.Product = p;
                            orderlines.Add(ol);
                        }
                        orderLineReader.Close();
                        cmd.Parameters.Clear();

                    }
                }
                catch (SqlException e) {
                    return null;
                }
            }
            return orderlines;
        }

        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
        public OrderLine Update(OrderLine Entity, bool test = false, bool testResult = false) {
            OrderLine orderLine = new OrderLine();
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
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

                                cmd.CommandText = "UPDATE Product " +
                                    "SET stock = @stock, sales = @sales WHERE productID = @productID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("stock", Entity.Product.Stock + 1);
                                cmd.Parameters.AddWithValue("sales", Entity.Product.Sales - 1);
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }

                                if (rowCount == 0) {
                                    orderLine.ErrorMessage = "Ordrelinjen blev ikke opdateret. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    orderLine.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException e) {
                        orderLine.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }
            }
            return orderLine;
        }

        // Creates orderline in database, affects product stock
        public OrderLine CreateInDesktop(OrderLine Entity, bool test = false, bool testResult = false) {
            OrderLine orderLine = new OrderLine();
            for (int i = 0; i < 5; i++) {
                try {
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

                                cmd.CommandText = "UPDATE Product " +
                                    "SET stock = @stock, sales = @sales WHERE productID = @productID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("stock", Entity.Product.Stock - Entity.Quantity);
                                cmd.Parameters.AddWithValue("sales", Entity.Product.Sales + Entity.Quantity);
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT INTO Orderline (Quantity, SubTotal, OrderID, ProductID) Values " +
                                                                "(@Quantity, @SubTotal, @OrderID, @ProductID)";
                                cmd.Parameters.AddWithValue("Quantity", Entity.Quantity);
                                cmd.Parameters.AddWithValue("SubTotal", Entity.SubTotal);
                                cmd.Parameters.AddWithValue("OrderID", Entity.OrderID);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }
                                if (rowCount == 0) {
                                    orderLine.ErrorMessage = "Ordrelinjen blev ikke oprettet. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    orderLine.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }

                        }
                    }
                }
                catch (SqlException e) {
                    orderLine.ErrorMessage = ErrorHandling.Exception(e);
                }
            }


            return orderLine;
        }

        // Deletes orderline from database, affects product stock
        public OrderLine DeleteInDesktop(OrderLine Entity, bool test = false, bool testResult = false) {
            OrderLine orderLine = new OrderLine();
            for (int i = 0; i < 5; i++) {
                try {
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
                                
                                cmd.CommandText = "UPDATE Product " +
                                    "SET stock = @stock, sales = @sales WHERE productID = @productID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("stock", Entity.Product.Stock + Entity.Quantity);
                                cmd.Parameters.AddWithValue("sales", Entity.Product.Sales - Entity.Quantity);
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                cmd.CommandText = "DELETE from Orderline WHERE OrderlineID = @OrderlineID";
                                cmd.Parameters.AddWithValue("OrderlineID", Entity.ID);
                                cmd.ExecuteNonQuery();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }
                                if (rowCount == 0) {
                                    orderLine.ErrorMessage = "Ordrelinjen blev ikke slettet. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    orderLine.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                }

                catch (SqlException e) {
                    orderLine.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return orderLine;
        }
    }
}


