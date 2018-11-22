using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Orderline {

        public Product Product { get; set; }
        public decimal SubTotal { get; set; }
        public int Quantity { get; set; }
        public long TimeStamp { get; set; }

        public Orderline(int quantity, decimal subTotal, Product p) {
            this.Quantity = quantity;
            this.SubTotal = subTotal;
            this.Product = p;
            TimeStamp = DateTime.Now.AddMinutes(30).Ticks;
        }
    }
}
