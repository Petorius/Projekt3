using System.Collections.Generic;
using Client.Domain;

namespace Client.ServiceLayer {
    public class ProductService : IProductService {

        ServiceReference1.ProductServiceClient myProxy;

        public ProductService() {
            myProxy = new ServiceReference1.ProductServiceClient();
        }

        public bool Create(string name, decimal price, int stock, int minStock, int maxStock,
                        string description, string ImageURL, string ImageName) {
            return myProxy.CreateProduct(name, price, stock, minStock, maxStock, description, ImageURL, ImageName);
        }

        public bool Delete(int id) {
            return myProxy.DeleteProduct(id);
        }

        public Product Find(int ID) {
            var p = myProxy.FindProduct(ID);
            Product product = new Product();
                if (p != null) {
                    product = BuildServiceProduct(p);
                }
            
            return product;
        }

        public IEnumerable<Product> GetAllProducts() {
            var products = myProxy.GetAllProducts();

            List<Product> productList = new List<Product>();

            if (products != null) {
                foreach (var p in products) {
                    Product product = BuildServiceProduct(p);
                    productList.Add(product);
                }
            }
            return productList;
        }

        // Helping method used to convert a product from server.domain to client.domain.
        private Product BuildServiceProduct(ServiceReference1.Product p) {
            Product product = new Product {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Description = p.Description,
                Rating = p.Rating,
                MinStock = p.MinStock,
                MaxStock = p.MaxStock,
                Sales = p.Sales
            };

            foreach (var i in p.Images) {
                Image image = new Image {
                    ImageSource = i.ImageSource,
                    Name = i.Name
                };
                product.Images.Add(image);
            }

            return product;
        }

        public bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            return myProxy.Update(ID, name, price, stock, minStock, maxStock, description, isActive);
        }
    }

}
