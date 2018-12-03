using Server.DataAccessLayer;
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
                        cmd.CommandText = "INSERT INTO review(text, dateCreated, productID, userID) VALUES(@Text, @DateCreated, @ProductID, @UserID)";
                        cmd.Parameters.AddWithValue("Text", review.Text);
                        cmd.Parameters.AddWithValue("DateCreated", review.DateCreated);
                        cmd.Parameters.AddWithValue("ProductID", productID);
                        cmd.Parameters.AddWithValue("UserID", userID);
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
            bool isDeleted = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    try {
                        cmd.CommandText = "Delete from Review where reviewID = @reviewID and review.userID = @userID";
                        cmd.Parameters.AddWithValue("reviewID", review.ID);
                        cmd.Parameters.AddWithValue("userID", review.User.ID);
                        cmd.ExecuteNonQuery();
                        isDeleted = true;
                    }
                    catch (SqlException) {
                        return isDeleted;
                    }
                }
            }
            return isDeleted;
        }


    }
}
