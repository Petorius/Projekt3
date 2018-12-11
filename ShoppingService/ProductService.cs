using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Server.Domain;
using Server.DataAccessLayer;
using DataAccessLayer.Interface;
using DataAccessLayer;
using BusniessLayer;

namespace Server.ServiceLayer {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService {
        private IProduct productDb;
        private ReviewDB reviewDB;
        private ProductLogic productLogic;

        public ProductService() {
            productDb = new ProductDB();
            reviewDB = new ReviewDB();
            productLogic = new ProductLogic();
        }

        public Product CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            Image i = new Image {
                ImageSource = ImageURL,
                Name = ImageName
            };
            p.Images.Add(i);

            return productDb.Create(p);
        }

        public Review CreateReview(string text, int productID, int userID) {
            Review review = new Review(text);
            return reviewDB.CreateReview(review, productID, userID);
        }

        public Product DeleteProduct(int id) {
            Product p = productLogic.GetProduct("productID", id.ToString());
            return productDb.Delete(p);
        }

        public Review DeleteReview(int reviewID, int reviewUserID) {
            Review r = new Review();
            r.User = new User();
            r.ID = reviewID;
            r.User.ID = reviewUserID;
            return reviewDB.Delete(r);
        }

        public Product GetProduct(string select, string input) {
            Product p = productLogic.GetProduct(select, input);
            return p;
        }

        public Product GetProductWithImages(string select, string input) {
            Product p = productLogic.GetProductWithImages(select, input);
            return p;
        }

        public Product GetProductWithReviews(string select, string input) {
            Product p = productLogic.GetProductWithReviews(select, input);
            return p;
        }

        public Product GetProductWithImagesAndReviews(string select, string input) {
            Product p = productLogic.GetProductWithImagesAndReviews(select, input);
            return p;
        }

        public Review FindReview(int ID) {
            Review r = reviewDB.Get(ID);
            return r;
        }

        public IEnumerable<Product> GetAllProductsWithImages() {
            return productLogic.GetAllProductsWithImages();
        }

        public Product Update(int id, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            p.IsActive = isActive;
            p.ID = id;
            return productDb.Update(p);
        }
    }
}
