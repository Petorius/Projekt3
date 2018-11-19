using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;
using Server.DataAccessLayer;
using DataAccessLayer.Interface;

namespace Server.BusinessLogic {
    public class OrderLogic {
        private IOrder orderDB;
        private ICRUD<Customer> customerDB;

        public OrderLogic() {
            orderDB = new OrderDB();
            customerDB = new CustomerDB();
        }
        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, IEnumerable<OrderLine> ol) {
            // Find kunde på mail. Hvis ikke findes laves en, hvis der findes updater database med evt. ny info

            // Lav invoice og gem i DB

            // Validere ol pris på hver Ol

            // Lav order med ol

            // Smid lortet sammen

            // Smid lortet i DB

            return null;
        }
    }
}
