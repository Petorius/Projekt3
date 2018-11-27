using System.Collections.Generic;
using Client.Domain;
using Client.ServiceLayer;

namespace Client.ControlLayer {
    public class ProductController {
        private IProductService productService;
        public ProductController() {
            productService = new ProductService();
        }

        public bool CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageUrl, string ImageName) {
            return productService.Create(name, price, stock, minStock, maxStock, description, ImageUrl, ImageName);
        }

        public Product Find(int ID) {
            return productService.Find(ID);
        }

        public bool DeleteProduct(int ID) {
            return productService.Delete(ID);
        }

        public bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            return productService.Update(ID, name, price, stock, minStock, maxStock, description, isActive);
        }

        public IEnumerable<Product> GetAllProducts() {
            return productService.GetAllProducts();
        }
    }
}
