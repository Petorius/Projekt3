using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;
using Client.ServiceLayer;

namespace Client.ControlLayer {
    public class ProductController {
        private IProductService productService;
        public ProductController() {
            productService = new ProductService();
        }

        public Product CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageUrl, string ImageName) {
            return productService.Create(name, price, stock, minStock, maxStock, description, ImageUrl, ImageName);
        }

        public Product Find(int ID) {
            Product p = productService.Find(ID);
            p.Reviews.Reverse();
            return p;
        }

        public Product FindByName(string name) {
            return productService.FindByName(name);
        }

        public Product DeleteProduct(int ID) {
            return productService.Delete(ID);
        }

        public Product Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            return productService.Update(ID, name, price, stock, minStock, maxStock, description, isActive);
        }

        public IEnumerable<Product> GetAllProducts() {
            return productService.GetAllProducts().OrderByDescending(x => x.Sales);
        }

        public Review CreateReview(string text, int productID, int userID) {
            return productService.CreateReview(text, productID, userID);
        }

        public  Review DeleteReview(int reviewID, int reviewUserID) {
            return productService.DeleteReview(reviewID, reviewUserID);
        }

        public Review FindReview(int ID) {
            Review r = productService.FindReview(ID);
            return r;
        }
    }
}
