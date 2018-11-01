using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain
{
    public class Product
    {
        public string _Name { get; set; }
        public double _Price { get; set; }
        public int _Stock { get; set; }
        public int _MinStock { get; set; }
        public int _MaxStock { get; set; }
        public string _Description { get; set; }
        public int _Rating { get; set; }
        public List<Copy> _Copies { get; set; }
        public Category _Category { get; set; }
        public List<Keyword> _Keywords { get; set; }
        public Review _Review { get; set; }

        public Product(string name, double price, int stock, int minStock, int maxStock, string description, List<Copy> copies, Category category, List<Keyword> keywords)
        {
            _Name = name;
            _Price = price;
            _Stock = stock;
            _MinStock = minStock;
            _MaxStock = maxStock;
            _Description = description;
            _Copies = copies;
            _Category = category;
            _Keywords = keywords;
        }

        public Product() {
                
        }
    }
}
