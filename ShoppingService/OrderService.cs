using Server.Domain;
using Server.DataAccessLayer;
using Server.BusinessLogic;
using System.Collections.Generic;
using DataAccessLayer.Interface;
using BusniessLayer;

namespace Server.ServiceLayer {

    public class OrderService : IOrderService {
        private IOrderLine orderLineDB;
        private IProduct productDB;
        private OrderLogic orderLogic;
        private ICRUD<Order> orderDB;
        private ProductLogic productLogic;

        public OrderService() {
            orderLineDB = new OrderLineDB();
            productDB = new ProductDB();
            orderLogic = new OrderLogic();
            orderDB = new OrderDB();
            productLogic = new ProductLogic();
        }

        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, List<OrderLine> ol) {
            return orderLogic.CreateOrder(firstName, lastName, street, zip, city, email,
            number, ol);
        }

        // Gets a product with images, builds an orderline with quantity, subtotal and product. Returns an orderline with error message.
        // This method does not create a orderline in database, used to affect product stock in database.
        public OrderLine CreateOrderLine(int quantity, decimal subTotal, int id) {
            Product p = productLogic.GetProductWithImages("productID", id.ToString());
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            OrderLine orderlineWithErrorMessage = orderLineDB.Create(ol);
            orderlineWithErrorMessage.Product = p;
            orderlineWithErrorMessage.Quantity = quantity;
            orderlineWithErrorMessage.SubTotal = subTotal;
            return orderlineWithErrorMessage;
        }

        // Used to create am orderline in database, affects product stock. Returns orderline with error message.
        public OrderLine CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID) {
            Product p = productLogic.GetProduct("productID", productID.ToString());
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            ol.OrderID = orderID;
            return orderLineDB.CreateInDesktop(ol);
        }

        public Order DeleteOrder(int ID) {
            Order o = orderLogic.GetOrder(ID);
            return orderLogic.DeleteOrder(o);
            
        }

        // Affects orderline product stock, returns orderline with error message
        public OrderLine DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            Product p = productLogic.GetProduct("productID", ID.ToString());
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Delete(ol);
        }

        // Deletes orderline in database, affects product stock. Returns orderline with error message
        public OrderLine DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID) {
            Product p = productLogic.GetProduct("productID", ID.ToString());
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            ol.ID = orderLineID;
            return orderLineDB.DeleteInDesktop(ol);
        }

        public Order FindOrder(int id) {
            return orderLogic.GetOrder(id);
        }

        public OrderLine FindOrderLine(int id) {
            return orderLineDB.Get(id);
        }

        // Updates orderline, affects product stock. Returns orderline with error message
        public OrderLine UpdateOrderLine(int ID, decimal subTotal, int quantity) {

            Product p = productLogic.GetProduct("productID", ID.ToString());
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Update(ol);
        }
    }
}