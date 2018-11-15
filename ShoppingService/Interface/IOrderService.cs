using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServiceLayer {
    [ServiceContract]
    interface IOrderService {
        [OperationContract]
        void CreateOrderLine(int quantity, decimal subTotal, int ID);
    }
}
