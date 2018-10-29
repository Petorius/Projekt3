using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Domain;
using DataAccessLayer;

namespace ShoppingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService {
        private ICRUD<Product> productDb;

        public ProductService() {
            productDb = new ProductDB();
        }
        public void CreateProduct(string name, double price, int stock, int minStock, int maxStock, string description) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            productDb.Create(p);
        }
    }
}
