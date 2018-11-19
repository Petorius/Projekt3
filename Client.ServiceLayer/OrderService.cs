using Client.Domain;
using Client.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer {
    public class OrderService : IOrderService {
        public Order CrateOrder(string firstName, string lastName, string street, int zip, string city, string email, 
            int number, IEnumerable<Orderline> ol) {
            OrderReference.OrderServiceClient myProxy = new OrderReference.OrderServiceClient();
            return myProxy.CreateOrder(firstName, lastName, street, zip, city, email,
            number, ol);
        }

        public bool CreateOrderLine(int quantity, decimal subTotal, int ID) {
            OrderReference.OrderServiceClient myProxy = new OrderReference.OrderServiceClient();
            return myProxy.CreateOrderLine(quantity, subTotal, ID);
        }

        public bool DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            OrderReference.OrderServiceClient myProxy = new OrderReference.OrderServiceClient();
            return myProxy.DeleteOrderLine(ID, subTotal, quantity);
        }

        public bool UpdateOrderLine(int ID, decimal subTotal, int quantity) {
            OrderReference.OrderServiceClient myProxy = new OrderReference.OrderServiceClient();
            return myProxy.UpdateOrderLine(ID, subTotal, quantity);
        }
    }
}