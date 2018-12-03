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

namespace Server.ServiceLayer {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService {
        private IProduct productDb;
        private ReviewDB reviewDB;

        public ProductService() {
            productDb = new ProductDB();
            reviewDB = new ReviewDB();
        }

        public bool CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            Image i = new Image {
                ImageSource = ImageURL,
                Name = ImageName
            };
            p.Images.Add(i);

            return productDb.Create(p);
        }

        public bool CreateReview(string text, int productID, int userID) {
            Review review = new Review(text);
            return reviewDB.CreateReview(review, productID, userID);
        }

        public bool DeleteProduct(int id) {
            Product p = productDb.Get(id);
            return productDb.Delete(p);
        }

        public bool DeleteReview(int reviewID, int reviewUserID, int sessionUserID) {
            Review r = new Review();
            r.User = new User();
            r.ID = reviewID;
            r.User.ID = reviewUserID;
            return reviewDB.DeleteReview(r);
        }

        public Product FindProduct(int ID) {
            Product product = productDb.Get(ID);
            return product;
        }

        public Product FindProductByName(string name) {
            Product product = productDb.GetByName(name);
            return product;
        }

        public IEnumerable<Product> GetAllProducts() {
            return productDb.GetAll();
        }

        public bool Update(int id, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            p.IsActive = isActive;
            p.ID = id;
            return productDb.Update(p);
        }
    }
}
