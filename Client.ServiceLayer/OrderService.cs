using Client.Domain;
using Client.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client.ServiceLayer {
    public class OrderService : IOrderService {

        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, IEnumerable<Orderline> ol) {
            OrderReference.OrderServiceClient myProxy = new OrderReference.OrderServiceClient();
            List<OrderReference.OrderLine> serviceOrderLines = GetServiceOrderLines(ol);

            var order = myProxy.CreateOrder(firstName, lastName, street, zip, city, email, number, serviceOrderLines.ToArray());

            return BuildOrderFromServices(order, ol.ToList());
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
        
        private List<OrderReference.OrderLine> GetServiceOrderLines(IEnumerable<Orderline> pols) {
            OrderReference.OrderLine tempOl;
            OrderReference.Product tempProduct;
            List<OrderReference.OrderLine> serviceOrderLines = new List<OrderReference.OrderLine>();

            foreach (Orderline pol in pols) {
                tempProduct = new OrderReference.Product() {
                    ID = pol.Product.ID,
                    Stock = pol.Product.Stock,
                    Price = pol.Product.Price
                };
                tempOl = new OrderReference.OrderLine() {
                    Product = tempProduct,
                    Quantity = pol.Quantity,
                    SubTotal = pol.SubTotal 
                };
                serviceOrderLines.Add(tempOl);
            }
            return serviceOrderLines;
        }

        private Order BuildOrderFromServices(OrderReference.Order order, List<Orderline> ol) {
            Customer c = new Customer(order.Customer.FirstName, order.Customer.LastName, order.Customer.Phone,
                order.Customer.Email, order.Customer.Address, order.Customer.ZipCode, order.Customer.City);
            Invoice i = new Invoice();
            Order o = new Order(c, i);
            o.Orderlines = ol;
            o.ID = order.ID;
            o.Total = order.Total;
            o.Validation = order.Validation;
            return o;
        }
           
    }
}