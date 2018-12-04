using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Server.Domain;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Server.DataAccessLayer {
    public class AdminDB {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public AdminDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public AdminDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public Admin GetAdmin(string email) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        Admin admin = new Admin();

                        cmd.CommandText = "select adminID, employeeEmail, hashPassword, salt from [dbo].[Admin] where [dbo].[Admin].EmployeeEmail = @Email ";
                        cmd.Parameters.AddWithValue("Email", email);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            admin.ID = reader.GetInt32(reader.GetOrdinal("adminID"));
                            admin.EmployeeEmail = reader.GetString(reader.GetOrdinal("employeeEmail"));
                            admin.HashPassword = reader.GetString(reader.GetOrdinal("hashPassword"));
                            admin.Salt = reader.GetString(reader.GetOrdinal("salt"));
                        }
                        reader.Close();
                        return admin;
                    }
                }
                catch (SqlException) {
                    return null;
                }

            }
        }

        public bool CreateAdmin(string email, string hashValue, string salt) {
            bool res = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {

                        cmd.CommandText = "Insert into [dbo].[Admin](EmployeeEmail, HashPassword, Salt) values (@EmployeeEmail, @HashPassword, @Salt)";
                        cmd.Parameters.AddWithValue("EmployeeEmail", email);
                        cmd.Parameters.AddWithValue("HashPassword", hashValue);
                        cmd.Parameters.AddWithValue("Salt", salt);
                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
                catch (SqlException) {
                    res = false;
                }
            }
            return res;
        }
    }
}
