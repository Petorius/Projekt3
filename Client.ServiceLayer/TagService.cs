using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer {
    public class TagService : ITagService {

        ServiceReference2.TagServiceClient myProxy;

        public TagService() {
            myProxy = new ServiceReference2.TagServiceClient();
        }

        public Tag FindTagByName(string name) {
            var t = myProxy.FindTagByName(name);
            Tag tag = new Tag();
            if (t != null) {
                foreach (var p in t.Products) {
                    Product product = new Product {
                        ID = p.ID,
                        Name = p.Name,
                        Price = p.Price,
                        Stock = p.Stock,
                        Description = p.Description,
                        Rating = p.Rating,
                        MinStock = p.MinStock,
                        MaxStock = p.MaxStock
                    };

                    foreach (var i in p.Images) {
                        Image image = new Image {
                            ImageSource = i.ImageSource,
                            Name = i.Name
                        };
                        product.Images.Add(image);
                    }

                    tag.Products.Add(product);
                }
            }
            return tag;
        }
    }
}
