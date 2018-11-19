﻿using Server.Domain;
using Server.DataAccessLayer;
using System;
using Server.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServiceLayer {

    public class OrderService : IOrderService {
        private ICRUD<OrderLine> orderLineDB;
        private ICRUD<Product> productDB;
        private OrderLogic orderLogic;

        public OrderService() {
            orderLineDB = new OrderLineDB();
            productDB = new ProductDB();
            orderLogic = new OrderLogic();
        }

        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, IEnumerable<OrderLine> ol) {
            return orderLogic.CreateOrder(firstName, lastName, street, zip, city, email,
            number, ol);
        }

        public bool CreateOrderLine(int quantity, decimal subTotal, int id) {
            Product p = productDB.Get(id);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Create(ol);
        }
    }
}