using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {
    [DataContract]
    public class Order {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public bool Validation { get; set; }
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public List<OrderLine> Orderlines { get; set; }

        public Order(Customer customer) {
            Validation = false;
            Customer = customer;
            Orderlines = new List<OrderLine>();
        }
    }

}
