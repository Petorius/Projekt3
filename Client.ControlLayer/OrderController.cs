using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceLayer;
using Client.ServiceLayer.Interface;

namespace Client.ControlLayer {
    public class OrderController {
        private IOrderService orderService;

        public OrderController() {
            orderService = new OrderService();
        }

        public void CreateOrderLine(int quantity, decimal subTotal, int ID) {
            orderService.CreateOrderLine(quantity,subTotal, ID);
        }
    }
}
