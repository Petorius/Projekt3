using System.Collections.Generic;
using Client.ServiceLayer;
using Client.ServiceLayer.Interface;
using Client.Domain;

namespace Client.ControlLayer {
    public class OrderController {
        private IOrderService orderService;

        public OrderController() {
            orderService = new OrderService();
        }

        public Orderline CreateOrderLine(int quantity, decimal subTotal, int ID) {
            return orderService.CreateOrderLine(quantity,subTotal, ID);
        }

        public Orderline CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID) {
            return orderService.CreateOrderLineInDesktop(quantity, subTotal, productID, orderID);
        }

        public Order CreateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, IEnumerable<Orderline> ol) {

            return orderService.CreateOrder(firstName, lastName, street, zip, city, email,
            number, ol);
        }

        public Order FindOrder(int id) {
            return orderService.FindOrder(id);
        }

        public Orderline FindOrderLine(int id) {
            return orderService.FindOrderLine(id);
        }

        public Orderline UpdateOrderLine(int ID, decimal subTotal, int quantity) {
            return orderService.UpdateOrderLine(ID, subTotal, quantity);
        }

        public Orderline DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            return orderService.DeleteOrderLine(ID, subTotal, quantity);
        }

        public Orderline DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID) {
            return orderService.DeleteOrderLineInDesktop(ID, subTotal, quantity, orderLineID);
        }

        public Order DeleteOrder(int ID) {
            return orderService.DeleteOrder(ID);
        }
    }
}
