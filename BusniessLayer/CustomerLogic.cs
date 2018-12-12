using DataAccessLayer.Interface;
using Server.DataAccessLayer;
using Server.Domain;

namespace Server.BusinessLogic {
    public class CustomerLogic {
        private ICustomer customerDB;

        public CustomerLogic() {
            customerDB = new CustomerDB();
        }

        // Database test constructor. Only used for testing.
        public CustomerLogic(string connectionString) {
            customerDB = new CustomerDB(connectionString);
        }

        // Checks if customer exists in the database. If customer exists we update,
        // otherwise we create a new customer.
        public Customer HandleCustomer(string firstName, string lastName, string street, int zip, string city, string email,
         int number) {
            Customer c = customerDB.Get("email", email);
            c.FirstName = firstName;
            c.LastName = lastName;
            c.Address = street;
            c.ZipCode = zip;
            c.City = city;
            c.Email = email;
            c.Phone = number;
            if (c.ID < 1) {
                c.ID = customerDB.Create(c).ID;
            }
            else {
                customerDB.Update(c);
            }
            return c;
        }
        
        public Customer UpdateCustomer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city, string existingemail) {
            Customer c = customerDB.Get("email", existingemail);
            c.FirstName = firstName;
            c.LastName = lastName;
            c.Address = address;
            c.ZipCode = zipCode;
            c.City = city;
            c.Email = email;
            c.Phone = phone;

            return customerDB.Update(c);
        }
    }
}
