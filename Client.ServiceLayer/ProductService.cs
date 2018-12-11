using System;
using System.Collections.Generic;
using Client.Domain;

namespace Client.ServiceLayer {
    public class ProductService : IProductService {

        ServiceReference1.ProductServiceClient myProxy;

        public ProductService() {
            myProxy = new ServiceReference1.ProductServiceClient();
        }

        public Product Create(string name, decimal price, int stock, int minStock, int maxStock,
                        string description, string ImageURL, string ImageName) {
            return BuildClientProduct(myProxy.CreateProduct(name, price, stock, minStock, maxStock, description, ImageURL, ImageName));
        }

        public Product Delete(int id) {
            return BuildClientProduct(myProxy.DeleteProduct(id));
        }

        public Product GetProduct(string select, string input) {
            var p = myProxy.GetProduct(select, input);
            Product product = new Product();
            if (p != null) {
                product = BuildClientProduct(p);
            }

            return product;
        }

        public Product GetProductWithImages(string select, string input) {
            var p = myProxy.GetProductWithImages(select, input);
            Product product = new Product();
            if (p != null) {
                product = BuildClientProduct(p);
            }

            return product;
        }

        public Product GetProductWithReviews(string select, string input) {
            var p = myProxy.GetProductWithReviews(select, input);
            Product product = new Product();
            if (p != null) {
                product = BuildClientProduct(p);
            }

            return product;
        }

        public Product GetProductWithImagesAndReviews(string select, string input) {
            var p = myProxy.GetProductWithImagesAndReviews(select, input);
            Product product = new Product();
            if (p != null) {
                product = BuildClientProduct(p);
            }

            return product;
        }
        
        public IEnumerable<Product> GetAllProductsWithImages() {
            var products = myProxy.GetAllProductsWithImages();

            List<Product> productList = new List<Product>();

            if (products != null) {
                foreach (var p in products) {
                    Product product = BuildClientProduct(p);
                    productList.Add(product);
                }
            }
            return productList;
        }

        // Helping method used to convert a product from server.domain to client.domain.
        private Product BuildClientProduct(ServiceReference1.Product p) {
            Product product = new Product {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Description = p.Description,
                Rating = p.Rating,
                MinStock = p.MinStock,
                MaxStock = p.MaxStock,
                Sales = p.Sales,
                IsActive = p.IsActive
                
            };
            product.ErrorMessage = p.ErrorMessage;
            

            foreach (var i in p.Images) {
                Image image = new Image {
                    ImageSource = i.ImageSource,
                    Name = i.Name
                };
                product.Images.Add(image);
            }

            foreach (var r in p.Reviews) {
                
                product.Reviews.Add(BuildClientReview(r));
            }

            return product;
        }

        public Product Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            return BuildClientProduct(myProxy.Update(ID, name, price, stock, minStock, maxStock, description, isActive));
        }
        
        public Review CreateReview(string text, int productID, int userID) {
            return BuildClientReview(myProxy.CreateReview(text, productID, userID));
        }

        public Review DeleteReview(int reviewID, int reviewUserID) {
            return BuildClientReview(myProxy.DeleteReview(reviewID, reviewUserID));
        }

        public Review FindReview(int ID) {
            var r = myProxy.FindReview(ID);
            Review review = new Review();
            if (r != null) {
                review = BuildClientReview(r);
            }
            return review;
        }

        private Review BuildClientReview(ServiceReference1.Review r) {
            Review review = new Review();
            review.User = new User();
            review.ID = r.ID;
            review.ErrorMessage = r.ErrorMessage;
            review.Text = r.Text;
            review.DateCreated = r.DateCreated;
            review.User.ID = r.User.ID;
            review.User.FirstName = r.User.FirstName;

            return review;
        }
    }
}