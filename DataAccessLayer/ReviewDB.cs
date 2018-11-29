﻿using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    public class ReviewDB : IReview {
        private string connectionString;

        public ReviewDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public bool CreateReview(Review review, int productID, int userID) {
            bool isCompleted = false;

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    try {
                        cmd.CommandText = "INSERT INTO review(text, productID, customerID) VALUES(@Text, @ProductID, @CustomerID)";
                        cmd.Parameters.AddWithValue("Text", review.Text);
                        cmd.Parameters.AddWithValue("ProductID", productID);
                        cmd.Parameters.AddWithValue("CustomerID", userID);
                        cmd.ExecuteNonQuery();
                        isCompleted = true;
                    }
                    catch (SqlException) {
                        isCompleted = false;
                    }
                }
            }
            return isCompleted;
        }

        public bool DeleteReview(Review review) {
            throw new NotImplementedException();
        }
    }
}
