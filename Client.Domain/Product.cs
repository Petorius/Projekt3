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
        public List<Image> Images { get; set; }
        public List<Copy> Copies { get; set; }
        public Category Category { get; set; }
        public List<Keyword> Keywords { get; set; }
        public Review Review { get; set; }

        public Product(string name, decimal price, int stock, int minStock, int maxStock, string description, List<Copy> copies, Category category, List<Keyword> keywords) {
            Name = name;
            Price = price;
            Stock = stock;
            MinStock = minStock;
            MaxStock = maxStock;
            Description = description;
            Copies = copies;
            Category = category;
            Keywords = keywords;
            
        }

        public Product() {
            Images = new List<Image>();

        }

        public Product(int id) {
            ID = id;
        }
    }
}
