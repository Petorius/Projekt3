using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer {
    public class TagService : ITagService {
        public Tag FindBestSellers(string name) {
            return FindTagByName(name);
        }

        public Tag FindTagByName(string name) {
            ServiceReference2.TagServiceClient myProxy = new ServiceReference2.TagServiceClient();
            var t = myProxy.FindTagByName(name);
            Tag tag = new Tag();
            if (t != null) {
                foreach (var p in t.Products) {
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

                    tag.Products.Add(product);
                }
                return tag;
            }
            return null;
        }

        public IEnumerable<Product> GetAllSales() {
            List<Product> allProducts = new List<Product>();
            ServiceReference2.TagServiceClient myProxy = new ServiceReference2.TagServiceClient();
            var s = myProxy.GetAllSales();
            if (s != null) {
                foreach (var p in s) {
                    Product product = new Product();
                    product.ID = p.ID;
                    product.Name = p.Name;
                    product.Price = p.Price;
                    product.Stock = p.Stock;
                    product.Description = p.Description;
                    product.Rating = p.Rating;
                    product.MinStock = p.MinStock;
                    product.MaxStock = p.MaxStock;
                    product.Sales = p.Sales;

                    foreach (var i in p.Images) {
                        Image image = new Image();
                        image.ImageSource = i.ImageSource;
                        image.Name = i.Name;
                        product.Images.Add(image);
                    }

                    allProducts.Add(product);
                }
                allProducts.OrderByDescending(product => product.Sales).ToList();

                return allProducts;
            }
            return null;

        }

        public Category GetSalesByCategory(string name) {
            ServiceReference2.TagServiceClient myProxy = new ServiceReference2.TagServiceClient();
            var c = myProxy.GetSalesByCategory(name);
            Category category = new Category();
            if (c != null) {
                foreach (var p in c.Products) {
                    Product product = new Product();
                    product.ID = p.ID;
                    product.Name = p.Name;
                    product.Price = p.Price;
                    product.Stock = p.Stock;
                    product.Description = p.Description;
                    product.Rating = p.Rating;
                    product.MinStock = p.MinStock;
                    product.MaxStock = p.MaxStock;
                    product.Sales = p.Sales;

                    foreach (var i in p.Images) {
                        Image image = new Image();
                        image.ImageSource = i.ImageSource;
                        image.Name = i.Name;
                        product.Images.Add(image);
                    }

                    category.Products.Add(product);
                }
                c.Products.OrderByDescending(product => product.Sales).ToList();
                return category;
            }
            return null;
        }

        public IEnumerable<Product> GetBestsellersInCategory(string name) {
            List<Product> CombinedList = new List<Product>();
            ServiceReference2.TagServiceClient myProxy = new ServiceReference2.TagServiceClient();
            Category c = GetSalesByCategory(name);
            Tag tag = FindBestSellers("Bestseller");

            foreach (Product product in c.Products) {
                if (tag.Products.Contains(product)) {
                    CombinedList.Add(product);
                }
            }
            return CombinedList;
        }
    }
}
