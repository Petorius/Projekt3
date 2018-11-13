using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Server.Domain;
using Server.DataAccessLayer;

namespace Server.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService {
        private ICRUD<Product> productDb;

        public ProductService() {
            productDb = new ProductDB();
        }
        public void CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            productDb.Create(p);
        }

        public bool DeleteProduct(int id) {
            return productDb.Delete(id);
        }

        public Product FindProduct(int ID) {
            Product product = productDb.Get(ID);
            return product;
        }

        public bool Update(int id, string name, decimal price, int stock, int minStock, int maxStock, string description) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            p.ID = id;
            return productDb.Update(p);
        }
    }
}
