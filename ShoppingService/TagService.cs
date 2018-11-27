using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;
using Server.DataAccessLayer;

namespace Server.ServiceLayer {
    public class TagService : ITagService {
        private TagDB tagDB;
        private ICRUD<Product> productDB;
        private CategoryDB categoryDB;

        public TagService() {
            tagDB = new TagDB();
            productDB = new ProductDB();
            categoryDB = new CategoryDB();
        }

        public Tag FindTagByName(string name) {
            return tagDB.Get(name);
        }

        public IEnumerable<Product> GetAllSales() {
            return productDB.GetAll();
        }

        public Category GetSalesByCategory(string name) {
            return categoryDB.Get(name);
        }

        public Tag FindBestSellers(string name) {
            return tagDB.Get(name);
        }

    }
}
