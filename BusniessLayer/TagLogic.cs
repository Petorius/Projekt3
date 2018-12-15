using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusniessLayer {
    public class TagLogic {

        ProductDB productDB;
        TagDB tagDB;

        public TagLogic() {
            productDB = new ProductDB();
            tagDB = new TagDB();
        }

        // Gets a specific tag with products by name
        public Tag GetTagWithProducts(string name) {
            Tag tag = new Tag();

            tag = tagDB.Get(name);

            // For every product in tag, builds images on product
            foreach(Product p in tag.Products) {
                p.Images = productDB.GetProductImages(p.ID);
            }
            return tag;
        }
    }
}
