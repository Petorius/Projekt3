using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Tag {

        public string ErrorMessage { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Tag() {
            Products = new List<Product>();
            ErrorMessage = "";
        }
    }
}
