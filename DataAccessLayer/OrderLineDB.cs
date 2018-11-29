﻿using Server.Domain;
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

        // Database test constructor. Only used for unit testing.
        public OrderLineDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public OrderLineDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
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

        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
        public bool Delete(OrderLine Entity, bool test = false, bool testResult = false) {
            bool deleted = false;
            for (int i = 0; i < 5; i++) {
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
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    cmd.Transaction.Commit();
                                }
                            }
                            catch (SqlException) {
                                deleted = false;
                            }

                        }
                    }

                }
            }
            return deleted;
        }

        public OrderLine Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderLine> GetAll() {
            throw new NotImplementedException();
        }

        // Method with optimistic concurreny. If anything is changed, we rollback our transaction after trying for 4 times
        public bool Update(OrderLine Entity, bool test = false, bool testResult = false) {
            bool isUpdated = false;
            for (int i = 0; i < 5; i++) {
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
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    isUpdated = true;
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                            catch (SqlException) {
                                isUpdated = false;
                            }

                        }
                    }
                }
            }
            return isUpdated;
        }
    }
}
