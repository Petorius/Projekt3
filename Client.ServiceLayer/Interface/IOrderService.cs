using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer.Interface {
    public interface IOrderService {
        bool CreateOrderLine(int quantity, decimal subTotal, int ID);
        bool UpdateOrderLine(int ID, decimal subTotal, int quantity);
    }
}
