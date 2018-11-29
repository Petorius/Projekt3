using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Product {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Sales { get; set; }
        public bool IsActive { get; set; }
        public List<Image> Images { get; set; }
        public List<Review> Reviews { get; set; }

        public Product(string name, decimal price, int stock, int minStock, int maxStock, string description) {
            Name = name;
            Price = price;
            Stock = stock;
            MinStock = minStock;
            MaxStock = maxStock;
            Description = description;
            Reviews = new List<Review>();
            Images = new List<Image>();
        }

        public Product() {
            Images = new List<Image>();
            Reviews = new List<Review>();
        }

        public Product(int id) {
            ID = id;
            Reviews = new List<Review>();
            Images = new List<Image>();
        }
    }
}
