﻿using DataAccessLayer.Interface;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public class CustomerDB : ICRUD<Customer>, ICustomer {
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

        public Customer Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll() {
            throw new NotImplementedException();
        }

        public bool Update(Customer Entity, bool test = false, bool testResult = false) {
            bool isUpdated = false;
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    using (SqlTransaction trans = connection.BeginTransaction()) {
                        byte[] rowId = null;
                        int rowCount = 0;
                        using (SqlCommand cmd = connection.CreateCommand()) {
                            cmd.Transaction = trans;
                            cmd.CommandText = "SELECT rowID FROM customer WHERE customerID = @customerID";
                            cmd.Parameters.AddWithValue("productID", Entity.ID);
                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read()) {
                                rowId = (byte[])reader["rowId"];
                            }
                            reader.Close();

                            try {
                                cmd.CommandText = "UPDATE Customer SET firstName = @firstName, lastname = @lastName, phone = @phone, " +
                                    "email = @email, address = @address, zipCode = @zipCode, city = @city WHERE cusomterID = @customerID AND rowID = @rowId";
                                cmd.Parameters.AddWithValue("firstName", Entity.FirstName);
                                cmd.Parameters.AddWithValue("lastName", Entity.LastName);
                                cmd.Parameters.AddWithValue("phone", Entity.Phone);
                                cmd.Parameters.AddWithValue("email", Entity.Email);
                                cmd.Parameters.AddWithValue("address", Entity.Address);
                                cmd.Parameters.AddWithValue("zipCode", Entity.ZipCode);
                                cmd.Parameters.AddWithValue("city", Entity.City);
                                cmd.Parameters.AddWithValue("rowID", rowId);
                                rowCount = cmd.ExecuteNonQuery();

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
                                return isUpdated;
                            }

                        }
                    }
                }
            }
            return isUpdated;
        }
    

        public Customer GetByMail(string email) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    Customer c = new Customer();

                    cmd.CommandText = "SELECT customerID, FirstName, LastName, Phone, Email, Address, ZipCode, City" +
                        " from Customer where Email = @Email";
                    cmd.Parameters.AddWithValue("Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        c.ID = reader.GetInt32(reader.GetOrdinal("customerID"));
                        c.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        c.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                        c.Phone = reader.GetInt32(reader.GetOrdinal("Phone"));
                        c.Email = reader.GetString(reader.GetOrdinal("Email"));
                        c.Address = reader.GetString(reader.GetOrdinal("Address"));
                        c.ZipCode = reader.GetInt32(reader.GetOrdinal("ZipCode"));
                        c.City = reader.GetString(reader.GetOrdinal("City"));
                    }
                    reader.Close();
                    return c;
                }

            }
        }

        public bool Delete(Customer Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }
    }
}
    
