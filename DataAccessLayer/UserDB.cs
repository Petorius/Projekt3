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

        public bool CreateUser(int key, string salt, string hashValue) {
            bool isCompleted = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {

                    cmd.CommandText = "Insert into [dbo].[User](UserID, HashPassword, Salt) values (@UserID, @HashPassword, @Salt)";
                    cmd.Parameters.AddWithValue("UserID", key);
                    cmd.Parameters.AddWithValue("HashPassword", hashValue);
                    cmd.Parameters.AddWithValue("Salt", salt);
                    cmd.ExecuteNonQuery();
                    isCompleted = true;

                    return isCompleted;
                }
            }
        }

        public User GetUser(string email) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    User user = new User();

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


                    return user;
                }
            }
        }
    }
}
