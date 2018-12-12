using System.Collections.Generic;
using Server.Domain;
using Server.DataAccessLayer;
using DataAccessLayer.Interface;
using System;
using DataAccessLayer;

namespace BusniessLayer {
    public class ProductLogic {

        ProductDB productDB;
        ReviewDB reviewDB;
        UserDB userDB;

        public ProductLogic() {
            productDB = new ProductDB();
            reviewDB = new ReviewDB();
            userDB = new UserDB();
        }

        public Product GetProduct(string select, string input) {
            Product p = productDB.Get(select, input);
            return p;
        }

        public Product GetProductWithImages(string select, string input) {
            Product p = GetProduct(select, input);
            p.Images = productDB.GetProductImages(p.ID);
            return p;
        }

        public Product GetProductWithReviews(string select, string input) {
            Product p = GetProduct(select, input);
            p.Reviews = BuildReviews(p.ID);
            return p;

        }

        public Product GetProductWithImagesAndReviews(string select, string input) {
            Product p = GetProductWithImages(select, input);
            p.Reviews = BuildReviews(p.ID);
            return p;     
        }

        private List<Review> BuildReviews(int id) {
            List<Review> reviews = new List<Review>();
            reviews = reviewDB.GetAll(id);

            foreach (Review r in reviews) {
                r.User = userDB.GetUser("userID", r.User.ID.ToString());
            }
            return reviews;
        }

        public IEnumerable<Product> GetAllProductsWithImages() {
            IEnumerable<Product> products = productDB.GetAll();

            foreach(Product p in products) {
                p.Images = productDB.GetProductImages(p.ID);
            }
            return products;
        }
    }
}
