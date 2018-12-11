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

        public Tag GetTagWithProducts(string name) {
            Tag tag = new Tag();

            tag = tagDB.Get(name);

            foreach(Product p in tag.Products) {
                p.Images = productDB.GetProductImages(p.ID);
            }
            return tag;
        }
    }
}
