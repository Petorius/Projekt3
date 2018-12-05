using Server.Domain;
using Server.DataAccessLayer;
using Server.BusinessLogic;
using System.Collections.Generic;
using DataAccessLayer.Interface;

namespace Server.ServiceLayer {

    public class OrderService : IOrderService {
        private IOrderLine orderLineDB;
        private IProduct productDB;
        private OrderLogic orderLogic;
        private ICRUD<Order> orderDB;

        public OrderService() {
            orderLineDB = new OrderLineDB();
            productDB = new ProductDB();
            orderLogic = new OrderLogic();
            orderDB = new OrderDB();
        }

        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, List<OrderLine> ol) {
            return orderLogic.CreateOrder(firstName, lastName, street, zip, city, email,
            number, ol);
        }

        public OrderLine CreateOrderLine(int quantity, decimal subTotal, int id) {
            Product p = productDB.Get(id);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Create(ol);
        }

        public OrderLine CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID) {
            Product p = productDB.Get(productID);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            ol.OrderID = orderID;
            return orderLineDB.CreateInDesktop(ol);
        }

        public Order DeleteOrder(int ID) {
            Order o = orderDB.Get(ID);
            return orderLogic.DeleteOrder(o);
            
        }

        public OrderLine DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            Product p = productDB.Get(ID);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Delete(ol);
        }

        public OrderLine DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID) {
            Product p = productDB.Get(ID);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            ol.ID = orderLineID;
            return orderLineDB.DeleteInDesktop(ol);
        }

        public Order FindOrder(int id) {
            return orderDB.Get(id);
        }

        public OrderLine FindOrderLine(int id) {
            return orderLineDB.Get(id);
        }

        public OrderLine UpdateOrderLine(int ID, decimal subTotal, int quantity) {

            Product p = productDB.Get(ID);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Update(ol);
        }
    }
}