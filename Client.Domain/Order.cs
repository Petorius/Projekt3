using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Order {
        public int ID { get; set; }
        public decimal Total { get; set; }
        public Customer Customer { get; set; }
        public Invoice Invoice { get; set; }
        public IEnumerable<Orderline> Orderlines { get; set; }

        public Order(Customer customer, Invoice invoice) {
            Customer = customer;
            Invoice = invoice;
        }
    }

}
