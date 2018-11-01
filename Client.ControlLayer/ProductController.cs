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

        public void CreateProduct(string name, double price, int stock, int minStock, int maxStock, string description) {
            productService.Create(name, price, stock, minStock, maxStock, description);
        }

        public Product Find(int ID) {
            
            return productService.Find(ID);
           
            
        }
    }
}
