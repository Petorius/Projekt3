using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Client.Domain;

namespace Client.Webshop.Models {
    public class Orderline {

        public Domain.Product Product { get; set; }
        public int Quantity { get; set; }

        public Orderline(Domain.Product product, int quantity) {
            this.Product = product;
            this.Quantity = quantity;
        }
    }
}