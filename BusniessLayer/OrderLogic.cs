using System.Collections.Generic;
using Server.Domain;
using Server.DataAccessLayer;
using DataAccessLayer.Interface;
using System;

namespace Server.BusinessLogic {
    public class OrderLogic {
        private OrderDB orderDB;
        private CustomerLogic cl;
        private IProduct productDB;
        private OrderLineDB orderLineDB;

        public OrderLogic() {
            orderDB = new OrderDB();
            cl = new CustomerLogic();
            productDB = new ProductDB();
            orderLineDB = new OrderLineDB();

        }

        // Database test constructor. Only used for testing.
        public OrderLogic(string connectionString) {
            orderDB = new OrderDB(connectionString);
            cl = new CustomerLogic(connectionString);
            productDB = new ProductDB(connectionString);
            orderLineDB = new OrderLineDB(connectionString);

        }

        // Creates an order with customer and returns an order based on validation
        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, List<OrderLine> ol) {

            // Checks if customer exists in the database. If customer exists we update,
            // otherwise we create a new customer.
            Customer c = cl.HandleCustomer(firstName, lastName, street, zip, city, email, number);

            Order o = new Order(c);

            // Validates the prices for each orderline on the order.
            if (ValidateOrderLinePrices(ol)) {
                o.Validation = true;
                o.Orderlines = ol;
                o.Total = TotalOrderPrice(ol);
                o.ID = orderDB.Create(o).ID;
            }
            else {
                o.Validation = false;
            }
            return o;
        }

        public Order DeleteOrder(Order o) {
            foreach (OrderLine ol in o.Orderlines) {
                Product p = productDB.Get(ol.Product.ID);
                ol.Product = p;
                orderLineDB.DeleteInDesktop(ol);
            }
            return orderDB.Delete(o);
        }

        // Helping method used to check if the prices on each orderline matches
        // with the prices in the database. Returns true if it matches and false if not.
        private bool ValidateOrderLinePrices(IEnumerable<OrderLine> ol) {
            bool result = false;
            foreach (OrderLine orderLine in ol) {
                Product p = productDB.Get(orderLine.Product.ID);
                decimal databasePrice = orderLine.Quantity * p.Price;
                if (databasePrice == orderLine.SubTotal) {
                    result = true;
                }
            }
            return result;
        }

        // Helping method used to calculate and return the total price of an order.
        private decimal TotalOrderPrice(IEnumerable<OrderLine> ol) {
            decimal total = 0;
            foreach (OrderLine orderLine in ol) {
                total += orderLine.SubTotal;
            }
            return total;
        }
    }
}