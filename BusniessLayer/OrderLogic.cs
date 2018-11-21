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
        private OrderDB orderDB;
        private CustomerDB customerDB;
        private ICRUD<Product> productDB;

        public OrderLogic() {
            orderDB = new OrderDB();
            customerDB = new CustomerDB();
            productDB = new ProductDB();
        }
        public Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email,
            int number, List<OrderLine> ol) {
            
            Customer c = HandleCustomer(firstName, lastName, street, zip, city, email, number);
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

        private Customer HandleCustomer(string firstName, string lastName, string street, int zip, string city, string email,
            int number) {
            Customer c = customerDB.GetByMail(email);
            c.FirstName = firstName;
            c.LastName = lastName;
            c.Address = street;
            c.ZipCode = zip;
            c.City = city;
            c.Email = email;
            c.Phone = number;
            if (c.ID < 1) {
                c.ID = customerDB.CreateReturnedID(c);
            } else {
                customerDB.Update(c);
            }
            return c;
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
