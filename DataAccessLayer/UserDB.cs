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
    public class UserDB : IUserDB {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public UserDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public UserDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public User CreateUser(int key, string salt, string hashValue) {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    try {
                        cmd.CommandText = "Insert into [dbo].[User](UserID, HashPassword, Salt) values (@UserID, @HashPassword, @Salt)";
                        cmd.Parameters.AddWithValue("UserID", key);
                        cmd.Parameters.AddWithValue("HashPassword", hashValue);
                        cmd.Parameters.AddWithValue("Salt", salt);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e) {
                        user.ErrorMessage = ErrorHandling.Exception(e);
                    }
                    return user;
                }
            }
        }

        public User GetUserWithOrders(string email) {
            User user = GetUser(email);
            
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "select orderID from [dbo].[order] where [dbo].[order].[customerID] = @userID";
                        cmd.Parameters.AddWithValue("userID", user.ID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read()) {
                            Order order = new Order();
                            order.ID = reader.GetInt32(reader.GetOrdinal("orderID"));
                            user.Orders.Add(order);
                        }
                        reader.Close();
                    }
                    if(user.Orders == null) {
                        user.ErrorMessage = "Kunden har ingen ordre";
                    }
                }
                catch (SqlException e) {
                    user.ErrorMessage = ErrorHandling.Exception(e);
                }

            }
            return user;
        }

        public User GetUser(string email) {
        User user = new User();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "select userid, firstName, lastName, phone, address, zipCode, city from [dbo].[user], customer where Customer.Email = @Email " +
                            "AND customer.CustomerID = [dbo].[User].UserID;";
                        cmd.Parameters.AddWithValue("Email", email);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            user.ID = reader.GetInt32(reader.GetOrdinal("userid"));
                            user.FirstName = reader.GetString(reader.GetOrdinal("firstName"));
                            user.LastName = reader.GetString(reader.GetOrdinal("lastName"));
                            user.Phone = reader.GetInt32(reader.GetOrdinal("phone"));
                            user.Email = email;
                            user.Address = reader.GetString(reader.GetOrdinal("address"));
                            user.ZipCode = reader.GetInt32(reader.GetOrdinal("zipCode"));
                            user.City = reader.GetString(reader.GetOrdinal("city"));
                        }
                        reader.Close();

                        cmd.CommandText = "SELECT HashPassword, Salt from [dbo].[User] where userID = @ID";
                        cmd.Parameters.AddWithValue("ID", user.ID);
                        SqlDataReader userReader = cmd.ExecuteReader();
                        while (userReader.Read()) {
                            user.HashPassword = userReader.GetString(userReader.GetOrdinal("HashPassword"));
                            user.Salt = userReader.GetString(userReader.GetOrdinal("Salt"));
                        }
                        userReader.Close();
                    }
                    if(user.ID < 1) {
                        user.ErrorMessage = "Brugeren findes ikke";
                    } 

                }
                catch (SqlException e) {
                    user.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
         return user;
        }

        public User DeleteUser(string email, bool test = false, bool testResult = false) {
            User user = new User();
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
                        connection.Open();
                        using (SqlTransaction trans = connection.BeginTransaction()) {
                            byte[] rowId = null;
                            int rowCount = 0;
                            using (SqlCommand cmd = connection.CreateCommand()) {
                                cmd.Transaction = trans;
                                cmd.CommandText = "SELECT rowID FROM customer WHERE customer.email = @email";
                                cmd.Parameters.AddWithValue("email", email);
                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read()) {
                                    rowId = (byte[])reader["rowId"];
                                }
                                reader.Close();
                                cmd.CommandText = "delete from Customer where customer.email = @email AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                // Used to unit test. If test is true, we can set rowCount to 0 and fake a optimistic concurreny problem
                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }

                                if (rowCount == 0) {
                                    user.ErrorMessage = "Brugeren blev ikke slettet. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    user.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException e) {
                        user.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }
            }
            return user;
        }
    }
}
