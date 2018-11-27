using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Server.Domain;
using Server.DataAccessLayer;

namespace Server.ServiceLayer {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService {
        private ICRUD<Product> productDb;

        public ProductService() {
            productDb = new ProductDB();
        }
        public bool CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName) {
            Product p = new Product(name, price, stock, minStock, maxStock, description);
            Image i = new Image();
            i.ImageSource = ImageURL;
            i.Name = ImageName;
            p.Images.Add(i);
            return productDb.Create(p);
        }

        public bool DeleteProduct(int id) {
            Product p = productDb.Get(id);
            return productDb.Delete(p);
        }

        public Product FindProduct(int ID) {
            Product product = productDb.Get(ID);
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
