using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {
    [DataContract]
    public class OrderLine {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal SubTotal { get; set; }
        [DataMember]
        public Product Product { get; set; }

        public OrderLine(int quantity, decimal subTotal, Product p) {
            Quantity = quantity;
            SubTotal = subTotal;
            Product = p;
        }

        public OrderLine() {

        }
    }
}
