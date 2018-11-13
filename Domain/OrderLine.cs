using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {
    public class OrderLine {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public Product Product { get; set; }

        public OrderLine(int quantity, decimal subTotal, Product p) {
            Quantity = quantity;
            SubTotal = subTotal;
            Product = p;
        }
    }
}
