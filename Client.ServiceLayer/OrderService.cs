using Client.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer
{
    public class OrderService : IOrderService
    {

        public void CreateOrderLine(int quantity, decimal subTotal, int ID)
        {
            //ServiceReference1.ProductServiceClient myProxy = new ServiceReference1.ProductServiceClient();
            //myProxy.CreateOrderLine(quantity, subTotal, ID);
        }
    }
}