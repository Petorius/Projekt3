using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Category : Tag {
        public Category()
        {
        }

        public Category(string name)
        {
            Name = name;
            Products = new List<Product>();
        }
    }


}
