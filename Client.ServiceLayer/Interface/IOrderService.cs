using Client.Domain;
using System.Collections.Generic;

namespace Client.ServiceLayer.Interface {
    public interface IOrderService {
        bool CreateOrderLine(int quantity, decimal subTotal, int ID);

        Order CreateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, IEnumerable<Orderline> ol);

        bool CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID);

        Order FindOrder(int id);

        Orderline FindOrderLine(int id);

        bool UpdateOrderLine(int ID, decimal subTotal, int quantity);

        bool DeleteOrderLine(int ID, decimal subTotal, int quantity);

        bool DeleteOrder(int ID);

        bool DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID);
    }
}
