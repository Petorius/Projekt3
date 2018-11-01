using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {
    [DataContract]
    public class Product {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public int Stock { get; set; }
        [DataMember]
        public int MinStock { get; set; }
        [DataMember]
        public int MaxStock { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Rating { get; set; }

        public Product(string name, double price, int stock, int minStock, int maxStock, string description) {
            Name = name;
            Price = price;
            Stock = stock;
            MinStock = minStock;
            MaxStock = maxStock;
            Description = description;
        }
    }
}
