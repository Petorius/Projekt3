using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;
using Server.DataAccessLayer;

namespace Server.BusinessLogic {
    public class OrderLogic {
        private OrderDB orderDB;
        private CustomerLogic cl;
        private ICRUD<Product> productDB;

        public OrderLogic() {
            orderDB = new OrderDB();
            cl = new CustomerLogic();
            productDB = new ProductDB();
        }

        // Database test constructor. Only used for unit testing.
        public OrderLogic(string connectionString) {
            orderDB = new OrderDB(connectionString);
            cl = new CustomerLogic(connectionString);
            productDB = new ProductDB(connectionString);

        }
        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, List<OrderLine> ol) {
            
            Customer c = cl.HandleCustomer(firstName, lastName, street, zip, city, email, number);
            Invoice i = new Invoice();

            Order o = new Order(c, i);

            if (ValidateOrderLinePrices(ol)) {
                o.Validation = true;
                o.Orderlines = ol;
                o.Total = TotalOrderPrice(ol);
                o.ID = orderDB.CreateReturnID(o);
        } else {
                o.Validation = false;
            }
            return o;
        }

        private bool ValidateOrderLinePrices(IEnumerable<OrderLine> ol) {
            //Foreach
            bool result = false;
            foreach  (OrderLine orderLine in ol) {
                Product p = productDB.Get(orderLine.Product.ID);
                decimal databasePrice = orderLine.Quantity * p.Price;
                if(databasePrice == orderLine.SubTotal) {
                    result = true;
                }
            }
            return result;
        }

        private decimal TotalOrderPrice(IEnumerable<OrderLine> ol) {
            decimal total = 0;
            foreach (OrderLine orderLine in ol) {
                total += orderLine.SubTotal;
            }
            return total;
        }
        
            
    }
}
