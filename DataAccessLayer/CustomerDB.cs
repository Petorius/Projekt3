using Server.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public class CustomerDB : ICRUD<Customer> {
        private string connectionString;

        // Database test constructor. Only used for unit testing.
        public CustomerDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public CustomerDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }
        public bool Create(Customer Entity, bool test = false, bool testResult = false) {
            bool isCompleted = true;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    try {
                        cmd.CommandText = "Insert into Customer(FirstName, LastName, Phone, Email, Address, ZipCode, City) values" +
                                " (@FirstName, @LastName, @Phone, @Email, @Address, @ZipCode, @City)";
                        cmd.Parameters.AddWithValue("FirstName", Entity.FirstName);
                        cmd.Parameters.AddWithValue("LastName", Entity.LastName);
                        cmd.Parameters.AddWithValue("Phone", Entity.Phone);
                        cmd.Parameters.AddWithValue("Email", Entity.Email);
                        cmd.Parameters.AddWithValue("Address", Entity.Address);
                        cmd.Parameters.AddWithValue("ZipCode", Entity.ZipCode);
                        cmd.Parameters.AddWithValue("City", Entity.City);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException) {
                        isCompleted = false;
                    }
                }
            }
            return isCompleted;
        }
    

        public bool Delete(int id, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }

        public Customer Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll() {
            throw new NotImplementedException();
        }

        public bool Update(Customer Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }
    }
}
