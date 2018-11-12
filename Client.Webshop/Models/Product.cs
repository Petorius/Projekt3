using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Webshop.Models
{
    public class Product
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        
    }
}