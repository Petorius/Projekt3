using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        public Product(string name, double price, int stock, int minStock, int maxStock, string description, int rating)
        {
            Name = name;
            Price = price;
            Stock = stock;
            MinStock = minStock;
            MaxStock = maxStock;
            Description = description;
            Rating = rating;
        }
    }
}
