using Server.Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IOrderService {

        [OperationContract]
        OrderLine CreateOrderLine(int quantity, decimal subTotal, int ID);

        [OperationContract]
        OrderLine CreateOrderLineInDesktop(int quantity, decimal subTotal, int productID, int orderID);

        [OperationContract]
        Order CreateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, List<OrderLine> ol);

        [OperationContract]
        Order FindOrder(int id);

        [OperationContract]
        OrderLine FindOrderLine(int id);

        [OperationContract]
        OrderLine UpdateOrderLine(int ID, decimal subTotal, int quantity);

        [OperationContract]
        OrderLine DeleteOrderLine(int ID, decimal subTotal, int quantity);

        [OperationContract]
        OrderLine DeleteOrderLineInDesktop(int ID, decimal subTotal, int quantity, int orderLineID);

        [OperationContract]
        Order DeleteOrder(int ID);
    }
}
