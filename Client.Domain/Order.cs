using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Order {
        public int ID { get; set; }
        public decimal Total { get; set; }
        public DateTime DateCreated { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Orderline> Orderlines { get; set; }
        public string ErrorMessage { get; set; }

        public Order(Customer customer) {
            Customer = customer;
            ErrorMessage = "";
        }

        public Order() {
            Orderlines = new List<Orderline>();
            ErrorMessage = "";
        }
    }

}
