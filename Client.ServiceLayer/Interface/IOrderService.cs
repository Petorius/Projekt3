using Client.Domain;
using System.Collections.Generic;

namespace Client.ServiceLayer.Interface {
    public interface IOrderService {
        Orderline CreateOrderLine(int quantity, decimal subTotal, int ID);
        Order CreateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, IEnumerable<Orderline> ol);
        Orderline CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID);
        Order FindOrder(int id);
        Orderline FindOrderLine(int id);
        Orderline UpdateOrderLine(int ID, decimal subTotal, int quantity);
        Orderline DeleteOrderLine(int ID, decimal subTotal, int quantity);
        Order DeleteOrder(int ID);
        Orderline DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID);
    }
}
