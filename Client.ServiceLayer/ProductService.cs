using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer {
    public class ProductService : IProductService {
        public bool Create(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            return myProxy.CreateProduct(name, price, stock, minStock, maxStock, description, ImageURL, ImageName);
        }

        public bool Delete(int id) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            return myProxy.DeleteProduct(id);
        }

        public Product Find(int ID) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            var p = myProxy.FindProduct(ID);
            Product product = new Product();
            if (p != null) {
                product.ID = p.ID;
                product.Name = p.Name;
                product.Price = p.Price;
                product.Stock = p.Stock;
                product.Description = p.Description;
                product.Rating = p.Rating;
                product.MinStock = p.MinStock;
                product.MaxStock = p.MaxStock;

                foreach(var i in p.Images) {
                    Image image = new Image();
                    image.ImageSource = i.ImageSource;
                    image.Name = i.Name;
                    product.Images.Add(image);
                }

                return product;
            }
            return null;

        }

        public IEnumerable<Product> GetAllProducts() {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            var products = myProxy.GetAllProducts();

            List<Product> productList = new List<Product>();

            if (products != null) {
                foreach (var p in products) {
                    Product product = new Product();
                    product.ID = p.ID;
                    product.Name = p.Name;
                    product.Price = p.Price;
                    product.Stock = p.Stock;
                    product.Description = p.Description;
                    product.Rating = p.Rating;
                    product.MinStock = p.MinStock;
                    product.MaxStock = p.MaxStock;

                    foreach (var i in p.Images) {
                        Image image = new Image();
                        image.ImageSource = i.ImageSource;
                        image.Name = i.Name;
                        product.Images.Add(image);
                    }
                    productList.Add(product);
                }
            }
            return productList;
        }

        public bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            return myProxy.Update(ID, name, price, stock, minStock, maxStock, description);
        }
    }
}
