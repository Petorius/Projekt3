using Client.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer {
    public class OrderService : IOrderService {

        public void CreateOrderLine(int quantity, decimal subTotal, int ID) {
            OrderReference.OrderServiceClient myProxy = new OrderReference.OrderServiceClient();
            myProxy.CreateOrderLine(quantity, subTotal, ID);
        }
    }
}