using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.ServiceLayer {
    [ServiceContract]
    interface IOrderService {
        [OperationContract]
        bool CreateOrderLine(int quantity, decimal subTotal, int ID);
        [OperationContract]
        Order CreateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, IEnumerable<OrderLine> ol);

        [OperationContract]
        bool UpdateOrderLine(int ID, decimal subTotal, int quantity);

        [OperationContract]
        bool DeleteOrderLine(int ID, decimal subTotal, int quantity);
    }
}
