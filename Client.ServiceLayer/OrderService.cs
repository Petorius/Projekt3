using Client.Domain;
using Client.ServiceLayer.Interface;
using System.Collections.Generic;
using System.Linq;


namespace Client.ServiceLayer {
    public class OrderService : IOrderService {
        OrderReference.OrderServiceClient myProxy;

        public OrderService() {
            myProxy = new OrderReference.OrderServiceClient();
        }

        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, IEnumerable<Orderline> ol) {
            List<OrderReference.OrderLine> serviceOrderLines = GetServiceOrderLines(ol);

            var order = myProxy.CreateOrder(firstName, lastName, street, zip, city, email, number, serviceOrderLines.ToArray());

            return BuildOrderFromServices(order, ol.ToList());
        }

        public Orderline CreateOrderLine(int quantity, decimal subTotal, int ID) {
            return BuildClientOrderline(myProxy.CreateOrderLine(quantity, subTotal, ID));
        }

        public Orderline DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            return BuildClientOrderline(myProxy.DeleteOrderLine(ID, subTotal, quantity));
        }

        public Orderline UpdateOrderLine(int ID, decimal subTotal, int quantity) {
            return BuildClientOrderline(myProxy.UpdateOrderLine(ID, subTotal, quantity));
        }
        
        // Helping method used to convert orderlines from Client.Domain to Server.Domain.
        private List<OrderReference.OrderLine> GetServiceOrderLines(IEnumerable<Orderline> orderlines) {
            OrderReference.OrderLine serviceOrderline;
            OrderReference.Product serviceProduct;
            List<OrderReference.OrderLine> serviceOrderLines = new List<OrderReference.OrderLine>();

            foreach (Orderline orderline in orderlines) {
                serviceProduct = new OrderReference.Product() {
                    ID = orderline.Product.ID,
                    Stock = orderline.Product.Stock,
                    Price = orderline.Product.Price
                };
                serviceOrderline = new OrderReference.OrderLine() {
                    Product = serviceProduct,
                    Quantity = orderline.Quantity,
                    SubTotal = orderline.SubTotal 
                };
                serviceOrderLines.Add(serviceOrderline);
            }
            return serviceOrderLines;
        }
        // Helping method used to convert OrderRefernce.Orderline to Client.Domain.Orderline
        private Orderline BuildClientOrderline(OrderReference.OrderLine orderline) {
            Orderline ol = new Orderline();
            if(orderline.Product != null) {
                Product p = new Product();
                p.ID = orderline.Product.ID;
                p.Stock = orderline.Product.Stock;
                p.Price = orderline.Product.Price;
                ol.Product = p;
            }
            ol.ID = orderline.ID;
            ol.Quantity = orderline.Quantity;
            ol.SubTotal = orderline.SubTotal;
            ol.ErrorMessage = orderline.ErrorMessage;
            return ol;
        }

        // Helping method used to convert an order from server.domain to client.domain.
        private Order BuildOrderFromServices(OrderReference.Order order, List<Orderline> ol) {

            Customer c = new Customer(order.Customer.FirstName, order.Customer.LastName, order.Customer.Phone,
                order.Customer.Email, order.Customer.Address, order.Customer.ZipCode, order.Customer.City);

            Order o = new Order(c) {
                Orderlines = ol,
                ID = order.ID,
                DateCreated = order.DateCreated,
                Total = order.Total,
            };
            o.ErrorMessage = order.ErrorMessage;
            return o;
        }

        public Order FindOrder(int id) {

            var o = myProxy.FindOrder(id);
            Order order = new Order();
            if (o != null) {
                order = BuildClientOrder(o);
            }

            return order;
        }

        // Helping method used to convert OrderReference.Order to Client.Domain.Order
        // Builds order with orderlines
        private Order BuildClientOrder(OrderReference.Order o) {

            List<Orderline> orderlines = new List<Orderline>();

            Customer c = new Customer {
                Email = o.Customer.Email
            };
            
            foreach (var ol in o.Orderlines) {

                Product p = new Product {
                    ID = ol.Product.ID
                };

                Orderline orderline = new Orderline {
                    ID = ol.ID,
                    Quantity = ol.Quantity,
                    SubTotal = ol.SubTotal,
                    Product = p
                };

                orderlines.Add(orderline);
            }

            Order order = new Order {
                ID = o.ID,
                Total = o.Total,
                DateCreated = o.DateCreated,
                Customer = c,
                Orderlines = orderlines
            };
            order.ErrorMessage = o.ErrorMessage;

            return order;
        }

        public Order DeleteOrder(int ID) {
            return BuildClientOrder(myProxy.DeleteOrder(ID));
        }

        public Orderline CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID) {
            return BuildClientOrderline(myProxy.CreateOrderLineInDesktop(quantity, subTotal, productID, orderID));
        }

        public Orderline DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID) {
            return BuildClientOrderline(myProxy.DeleteOrderLineInDesktop(ID, subTotal, quantity, orderLineID));
        }

        // Gets orderlne and builds Client.Domain.Product
        public Orderline FindOrderLine(int id) {

            var ol = myProxy.FindOrderLine(id);
            Orderline orderline = new Orderline();

            if (ol != null) {
                Product p = new Product {
                    ID = ol.Product.ID
                };

                orderline.ID = ol.ID;
                orderline.Quantity = ol.Quantity;
                orderline.SubTotal = ol.SubTotal;
                orderline.Product = p;
            }
                
            return orderline;
        }
    }
}