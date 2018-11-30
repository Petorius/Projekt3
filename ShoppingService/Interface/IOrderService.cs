using Server.Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IOrderService {

        [OperationContract]
        bool CreateOrderLine(int quantity, decimal subTotal, int ID);

        [OperationContract]
        bool CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID);

        [OperationContract]
        Order CreateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, List<OrderLine> ol);

        [OperationContract]
        Order FindOrder(int id);

        [OperationContract]
        OrderLine FindOrderLine(int id);

        [OperationContract]
        bool UpdateOrderLine(int ID, decimal subTotal, int quantity);

        [OperationContract]
        bool DeleteOrderLine(int ID, decimal subTotal, int quantity);

        [OperationContract]
        bool DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID);

        [OperationContract]
        bool DeleteOrder(int ID);
    }
}
