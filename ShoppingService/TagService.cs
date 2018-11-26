using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;
using Server.DataAccessLayer;

namespace Server.ServiceLayer {
    public class TagService : ITagService {
        TagDB tagDB;
        SalesDB salesDB;


        public TagService() {
            tagDB = new TagDB();
            salesDB = new SalesDB();
        }

        public Tag FindTagByName(string name) {
            return tagDB.Get(name);
        }

        public IEnumerable<Product> GetAllSales()
        {
            return salesDB.GetAllSales();
        }

        public Category GetSalesByCategory(string name)
        {
            return salesDB.GetSalesByCategory(name);
        }

        public Tag FindBestSellers(string name)
        {
            return tagDB.Get(name);
        }

    }
}
