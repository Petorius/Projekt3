using Server.DataAccessLayer;
using Server.Domain;
using System.Collections.Generic;
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

        public Review Create(Review Entity, bool test = false, bool testResult = false) {
            throw new System.NotImplementedException();
        }

        public Review CreateReview(Review review, int productID, int userID, bool test = false, bool testResult = false) {
            Review errorReview = new Review();
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
                    }
                }
                catch (SqlException e) {
                    errorReview.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return errorReview;
        }

        public Review Delete(Review Entity, bool test = false, bool testResult = false) {
            Review review = new Review();
            review.User = new User();
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
                                cmd.Parameters.AddWithValue("reviewID", Entity.ID);
                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read()) {
                                    rowId = (byte[])reader["rowId"];
                                }
                                reader.Close();

                                cmd.CommandText = "Delete from Review where reviewID = @reviewID and review.userID = @userID and rowId = @rowId";
                                cmd.Parameters.AddWithValue("userID", Entity.User.ID);
                                cmd.Parameters.AddWithValue("rowId", rowId);
                                rowCount = cmd.ExecuteNonQuery();

                                if (test) {
                                    rowCount = testResult ? 1 : 0;
                                }

                                if (rowCount == 0) {
                                    review.ErrorMessage = "Anmeldelsen blev ikke slettet. Prøv igen";
                                    cmd.Transaction.Rollback();
                                }
                                else {
                                    review.ErrorMessage = "";
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException e) {
                        review.ErrorMessage = ErrorHandling.Exception(e);
                    }
                }
            }
            return review;
        }
        
        public Review Get(int id) {
            Review r = new Review();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        
                        r.User = new User();

                        cmd.CommandText = "SELECT reviewid, text, dateCreated, userID from Review where reviewID = @reviewID";
                        cmd.Parameters.AddWithValue("reviewID", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            r.ID = reader.GetInt32(reader.GetOrdinal("reviewid"));
                            r.Text = reader.GetString(reader.GetOrdinal("text"));
                            r.DateCreated = reader.GetDateTime(reader.GetOrdinal("dateCreated"));
                            r.User.ID = reader.GetInt32(reader.GetOrdinal("userID"));
                            
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                        
                        if(r.ID < 1) {
                            r.ErrorMessage = "Anmeldelsen findes ikke";
                        }
                        
                    }
                }
                catch (SqlException e) {
                    r.ErrorMessage = ErrorHandling.Exception(e);
                }
            }
            return r;
        }

        public IEnumerable<Review> GetAll() {
            throw new System.NotImplementedException();
        }

        public Review Update(Review Entity, bool test = false, bool testResult = false) {
            throw new System.NotImplementedException();
        }
    }
}
