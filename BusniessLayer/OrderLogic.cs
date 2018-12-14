using System.Collections.Generic;
using Server.Domain;
using Server.DataAccessLayer;
using DataAccessLayer.Interface;
using System;
using BusniessLayer;

namespace Server.BusinessLogic {
    public class OrderLogic {
        private OrderDB orderDB;
        private CustomerLogic cl;
        private IProduct productDB;
        private OrderLineDB orderLineDB;
        private ProductLogic productLogic;
        private CustomerDB customerDB;

        public OrderLogic() {
            orderDB = new OrderDB();
            cl = new CustomerLogic();
            productDB = new ProductDB();
            orderLineDB = new OrderLineDB();
            productLogic = new ProductLogic();
            customerDB = new CustomerDB();

        }

        // Database test constructor. Only used for testing.
        public OrderLogic(string connectionString) {
            orderDB = new OrderDB(connectionString);
            cl = new CustomerLogic(connectionString);
            productDB = new ProductDB(connectionString);
            orderLineDB = new OrderLineDB(connectionString);
            productLogic = new ProductLogic(connectionString);
            customerDB = new CustomerDB(connectionString);
        }

        // Creates an order with customer and returns an order based on validation
        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, List<OrderLine> ol) {

            // Checks if customer exists in the database. If customer exists we update,
            // otherwise we create a new customer.
            Customer c = cl.HandleCustomer(firstName, lastName, street, zip, city, email, number);

            Order o = new Order(c);

            // Validates the prices for each orderline on the order.
            o.Orderlines = ol;
            o.Total = TotalOrderPrice(ol);
            o.ID = orderDB.Create(o).ID;

            return o;
        }

        // Gets an order with customer and orderlines
        public Order GetOrder(int id) {

            Order o = orderDB.Get(id);
            
            o.Customer = customerDB.Get("customerID", o.Customer.ID.ToString());
            
            o.Orderlines = orderLineDB.GetOrderlinesByOrderID(o.ID);
            
            return o;
        }

        // Deletes order from database
        public Order DeleteOrder(Order o) {
            foreach (OrderLine ol in o.Orderlines) {
                Product p = productLogic.GetProduct("productID", ol.Product.ID.ToString());
                ol.Product = p;
                orderLineDB.DeleteInDesktop(ol);
            }
            return orderDB.Delete(o);
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