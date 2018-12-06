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
            return BuildClientOrderlines(myProxy.CreateOrderLine(quantity, subTotal, ID));
        }

        public Orderline DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            return BuildClientOrderlines(myProxy.DeleteOrderLine(ID, subTotal, quantity));
        }

        public Orderline UpdateOrderLine(int ID, decimal subTotal, int quantity) {
            return BuildClientOrderlines(myProxy.UpdateOrderLine(ID, subTotal, quantity));
        }
        
        // Helping method used to convert orderlines from client.Domain to server.Domain.
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

        private Orderline BuildClientOrderlines(OrderReference.OrderLine orderline) {
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
                Validation = order.Validation
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

            return order;
        }

        public Order DeleteOrder(int ID) {
            return BuildClientOrder(myProxy.DeleteOrder(ID));
        }

        public Orderline CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID) {
            return BuildClientOrderlines(myProxy.CreateOrderLineInDesktop(quantity, subTotal, productID, orderID));
        }

        public Orderline DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID) {
            return BuildClientOrderlines(myProxy.DeleteOrderLineInDesktop(ID, subTotal, quantity, orderLineID));
        }

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