using Client.Domain;
using Client.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer {
    public class OrderService : IOrderService {
        public Order CrateOrder(string firstName, string lastName, string street, int zip, string city, string email, int number, IEnumerable<Orderline> ol) {
            throw new NotImplementedException();

        }

        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email, 
            int number, IEnumerable<Orderline> ol) {
            OrderReference.OrderServiceClient myProxy = new OrderReference.OrderServiceClient();

            //OrderReference.OrderLine[] serverOrderLines = GetServerOrderLine(ol);

            //var order = myProxy.CreateOrder(firstName, lastName, street, zip, city, email,
            //number, serverOrderLines);

            //Customer customer = new Customer(order.Customer.FirstName, order.Customer.LastName, order.Customer.Phone, 
            //    order.Customer.Email, order.Customer.Address, order.Customer.ZipCode, order.Customer.City);
            //Order order2 = new Order(customer, new Invoice());
            //order2.Total = order.Total;
            //order2.ID = order.ID;
            //order2.Orderlines = ol;
            //return order2;
        }

        //private OrderReference.OrderLine[] GetServerOrderLine(IEnumerable<Orderline> ol) {
        //    List<OrderReference.OrderLine[]> orderLines = new List<OrderReference.OrderLine[]>();

        //    foreach (Orderline orderline in ol) {
        //        OrderReference.Product product = new OrderReference.Product();
        //        product.ID = orderline.Product.ID;
        //        orderLines.Add(new OrderReference.OrderLine(orderline.Quantity, orderline.SubTotal, product));
        //    }

        //    return orderLines;
        //}
            
        

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