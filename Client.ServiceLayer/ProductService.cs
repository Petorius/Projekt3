using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer
{
    public class ProductService : IProductService {
        public void Create(string name, double price, int stock, int minStock, int maxStock, string description) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            myProxy.CreateProduct(name, price, stock, minStock, maxStock, description);
        }

        public Product Find(int ID) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            var p = myProxy.FindProduct(ID);

            Product product = new Product();
            product._Name = p.Name;
            product._Price = p.Price;
            product._Stock = p.Stock;
            product._Description = p.Description;
            product._Rating = p.Rating;
            return product;
        }
    }
}
