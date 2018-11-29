using Server.Domain;
using Server.DataAccessLayer;
using Server.BusinessLogic;
using System.Collections.Generic;
using DataAccessLayer.Interface;

namespace Server.ServiceLayer {

    public class OrderService : IOrderService {
        private ICRUD<OrderLine> orderLineDB;
        private IProduct productDB;
        private OrderLogic orderLogic;

        public OrderService() {
            orderLineDB = new OrderLineDB();
            productDB = new ProductDB();
            orderLogic = new OrderLogic();
        }

        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, List<OrderLine> ol) {
            return orderLogic.CreateOrder(firstName, lastName, street, zip, city, email,
            number, ol);
        }

        public bool CreateOrderLine(int quantity, decimal subTotal, int id) {
            Product p = productDB.Get(id);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Create(ol);
        }

        public bool DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            Product p = productDB.Get(ID);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Delete(ol);
        }

        public bool UpdateOrderLine(int ID, decimal subTotal, int quantity) {

            Product p = productDB.Get(ID);
            OrderLine ol = new OrderLine(quantity, subTotal, p);
            return orderLineDB.Update(ol);
        }
    }
}