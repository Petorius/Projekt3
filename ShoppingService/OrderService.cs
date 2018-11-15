using Server.Domain;
using Server.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServiceLayer {

    public class OrderService : IOrderService {
        private ICRUD<OrderLine> orderLineDB;
        private ICRUD<Product> productDB;

        public OrderService() {
            orderLineDB = new OrderLineDB();
            productDB = new ProductDB();
        }

        public bool CreateOrderLine(int quantity, decimal subTotal, int id) {
            Product p = productDB.Get(id);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Create(ol);
        }
    }
}