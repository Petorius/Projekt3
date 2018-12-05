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
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Price { get; set; }
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
        [DataMember]
        public int Sales { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public List<Image> Images { get; set; }
        [DataMember]
        public List<Review> Reviews { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }

        public Product(string name, decimal price, int stock, int minStock, int maxStock, string description) {
            Name = name;
            Price = price;
            Stock = stock;
            MinStock = minStock;
            MaxStock = maxStock;
            Description = description;
            Images = new List<Image>();
            Reviews = new List<Review>();
            ErrorMessage = "";
        }

        public Product() {
            Images = new List<Image>();
            Reviews = new List<Review>();
            ErrorMessage = "";
        }
    }
}
