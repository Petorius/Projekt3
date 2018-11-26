using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DataAccessLayer;
using Server.Domain;

namespace Server.BusinessLogic {
    public class CustomerLogic {
        private CustomerDB customerDB;

        public CustomerLogic() {
            customerDB = new CustomerDB();
        }

        // Database test constructor. Only used for unit testing.
        public CustomerLogic(string connectionString) {
            customerDB = new CustomerDB(connectionString);
        }

        public Customer HandleCustomer(string firstName, string lastName, string street, int zip, string city, string email,
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
            }
            else {
                customerDB.Update(c);
            }
            return c;
        }

        public Customer GetCustomerByMail(string email) {
            return customerDB.GetByMail(email);
        }
    }
}
