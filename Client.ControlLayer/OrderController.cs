using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceLayer;
using Client.ServiceLayer.Interface;
using Client.Domain;

namespace Client.ControlLayer {
    public class OrderController {
        private IOrderService orderService;

        public OrderController() {
            orderService = new OrderService();
        }

        public bool CreateOrderLine(int quantity, decimal subTotal, int ID) {
            return orderService.CreateOrderLine(quantity,subTotal, ID);
        }

        public Order CreateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, IEnumerable<Orderline> ol) {
            return orderService.CrateOrder(firstName, lastName, street, zip, city, email,
            number, ol);
        }

        public bool UpdateOrderline(int ID, decimal subTotal, int quantity) {
            return orderService.UpdateOrderLine(ID, subTotal, quantity);
        }

        public bool DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            return orderService.DeleteOrderLine(ID, subTotal, quantity);
        }
    }
}
