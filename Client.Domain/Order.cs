using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Order {
        public int ID { get; set; }
        public decimal Total { get; set; }
        public bool Validation { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Orderline> Orderlines { get; set; }

        public Order(Customer customer) {
            Customer = customer;
        }

        public Order() {

        }
    }

}
