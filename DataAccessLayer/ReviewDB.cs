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
    public class ReviewDB : ICRUD<Review> {
        private string connectionString;

        public ReviewDB() {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public bool Create(Review Entity, bool test = false, bool testResult = false) {
            int insertedID = -1;
            bool isCompleted = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {

                    cmd.CommandText = "Insert into Review(Name, Description, ProductID, ReviewerEmail) OUTPUT INSERTED.ReviewID values" +
                            " (@Name, @Description, @ProductID, @ReviewerEmail)";
                    cmd.Parameters.AddWithValue("Name", Entity.Name);
                    cmd.Parameters.AddWithValue("Description", Entity.Description);
                    cmd.Parameters.AddWithValue("ProductID", Entity.ProductID);
                    cmd.Parameters.AddWithValue("ReviewerEmail", Entity.ReviewerEmail);
                    insertedID = (int)cmd.ExecuteScalar();

                }

                if (insertedID > 0) {
                    isCompleted = true;
                }
            }
            return isCompleted;
        }


        public bool Delete(Review Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }

        public Review Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll() {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetProductReviews(int productID) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                List<Review> productReviews = new List<Review>();
                using (SqlCommand cmd = connection.CreateCommand()) {


                    cmd.CommandText = "SELECT * from Review where ProductID = @ProductID";
                    cmd.Parameters.AddWithValue("ProductID", productID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Review review = new Review();
                        review.ReviewID = reader.GetInt32(reader.GetOrdinal("reviewID"));
                        review.Name = reader.GetString(reader.GetOrdinal("name"));
                        review.Description = reader.GetString(reader.GetOrdinal("description"));
                        review.ReviewerEmail = reader.GetString(reader.GetOrdinal("reviewerEmail"));
                        productReviews.Add(review);
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
                }
            return productReviews;

            }
        }

            public bool Update(Review Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }
    }
}
