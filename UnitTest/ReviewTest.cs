using System;
using Server.Domain;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.BusinessLogic;
using Server.DataAccessLayer;

namespace UnitTest {
    [TestClass]
    public class ReviewTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private ReviewDB reviewDB;
        private UserLogic userLogic;
        private UserDB userDB;
        private ProductDB productDB;
        private User user;

        [TestInitialize]
        public void SetUp() {
            reviewDB = new ReviewDB(connectionString);
            userLogic = new UserLogic(connectionString);
            userDB = new UserDB(connectionString);
            productDB = new ProductDB(connectionString);
            user = userDB.GetUser("g-star-raw@gmail.gcom");

            if(user.ID < 1) {
                userLogic.CreateUserWithPassword("Rune", "G", "G-Street", 9000, "G-Borg", 
                                            "g-star-raw@gmail.gcom", 81238123, "SuperTester123!");
            }
        }

        [TestMethod]
        public void CreateReview() {
            Review review = new Review();
            Product product = productDB.Get(1);
            review.Text = "Testing 123 testing";

            Review r = reviewDB.CreateReview(review, product.ID, user.ID);

            Assert.AreEqual(r.ErrorMessage, "");
        }

        [TestMethod]
        public void DeleteReviewExpectedToFail() {
            Review review = new Review();
            review.Text = "123123";
            review.User = user;
            Product product = productDB.Get(1);

            reviewDB.CreateReview(review, product.ID, user.ID);

            Review r = reviewDB.Delete(review, true, true);

            Assert.AreEqual(r.ErrorMessage, "");
        }

        [TestMethod]
        public void DeleteReview() {
            Review review = new Review();
            review.Text = "123123";
            review.User = user;
            Product product = productDB.Get(1);

            reviewDB.CreateReview(review, product.ID, user.ID);

            Review r = reviewDB.Delete(review, true);

            Assert.AreEqual(r.ErrorMessage, "");
        }

        [TestMethod]
        public void FindReview() {

            Review r = reviewDB.Get(1);

            Assert.IsNotNull(r);
            
        }
    }
}
