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

        public bool CreateOrderLine(int quantity, decimal subTotal, int ID) {
            return orderService.CreateOrderLine(quantity,subTotal, ID);
        }
    }
}
