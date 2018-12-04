using Server.DataAccessLayer;
using Server.Domain;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer {
    public class ReviewDB : IReview {
        private string connectionString;

        public ReviewDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        // Used for unit testing
        public ReviewDB(string connectionString) {
            this.connectionString = connectionString;
        }

        public bool CreateReview(Review review, int productID, int userID, bool test = false, bool testResult = false) {
            bool isCompleted = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {

                        cmd.CommandText = "INSERT INTO review(text, dateCreated, productID, userID) VALUES(@Text, @DateCreated, @ProductID, @UserID)";
                        cmd.Parameters.AddWithValue("Text", review.Text);
                        cmd.Parameters.AddWithValue("DateCreated", review.DateCreated);
                        cmd.Parameters.AddWithValue("ProductID", productID);
                        cmd.Parameters.AddWithValue("UserID", userID);
                        cmd.ExecuteNonQuery();
                        isCompleted = true;
                    }
                }
                catch (SqlException) {
                    isCompleted = false;
                }
            }
            return isCompleted;
        }

        public bool DeleteReview(Review review, bool test = false, bool testResult = false) {
            bool isDeleted = false;
            for (int i = 0; i < 5; i++) {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    try {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction()) {
                            byte[] rowId = null;
                            int rowCount = 0;
                            using (SqlCommand cmd = connection.CreateCommand()) {
                                cmd.Transaction = transaction;
                                cmd.CommandText = "SELECT rowID from Review WHERE reviewID = @reviewID";
                                cmd.Parameters.AddWithValue("reviewID", review.ID);
                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read()) {
                                    rowId = (byte[])reader["rowId"];
                                }
                                reader.Close();

                                cmd.CommandText = "Delete from Review where reviewID = @reviewID and review.userID = @userID and rowId = @rowId";
                                cmd.Parameters.AddWithValue("userID", review.User.ID);
                                cmd.Parameters.AddWithValue("rowId", rowId);
                                rowCount = cmd.ExecuteNonQuery();
                                isDeleted = true;

                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }

                                if (rowCount == 0) {
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    isDeleted = true;
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException) {
                        isDeleted = false;
                    }
                }
            }
            return isDeleted;
        }
    }
}
