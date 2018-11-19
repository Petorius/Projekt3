using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;
namespace Client.ServiceLayer.Interface {
    public interface IOrderService {
        bool CreateOrderLine(int quantity, decimal subTotal, int ID);
        Order CrateOrder(string firstName, string lastName, string street,
            int zip, string city, string email, int number, IEnumerable<Orderline> ol);
        bool UpdateOrderLine(int ID, decimal subTotal, int quantity);
        bool DeleteOrderLine(int ID, decimal subTotal, int quantity);
    }
}
