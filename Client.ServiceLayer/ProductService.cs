﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer {
    public class ProductService : IProductService {
        public void Create(string name, decimal price, int stock, int minStock, int maxStock, string description) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            myProxy.CreateProduct(name, price, stock, minStock, maxStock, description);
        }

        public bool Delete(int id) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            return myProxy.DeleteProduct(id);
        }

        public Product Find(int ID) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            var p = myProxy.FindProduct(ID);
            Product product = new Product();
            if (p != null) {
                product.Name = p.Name;
                product.Price = p.Price;
                product.Stock = p.Stock;
                product.Description = p.Description;
                product.Rating = p.Rating;
                return product;
            }
            return null;
            
        }

        public void Update(int ID) {
            ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            myProxy.Update(ID);

        }
    }
}
