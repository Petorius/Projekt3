using Client.Domain;

namespace Client.ServiceLayer {
    public class UserService : IUserService {

        UserReference.UserServiceClient myProxy;

        public UserService() {
            myProxy = new UserReference.UserServiceClient();
        }

        public User GetUser(string email) {
            return BuildUser(myProxy.GetUser(email));
        }

        public Customer GetCustomerByEmail(string email) {
            var customer = myProxy.GetCustomerByMail(email);
            Customer c = new Customer();
            if (customer != null) {
                c.ID = customer.ID;
                c.FirstName = customer.FirstName;
                c.LastName = customer.LastName;
                c.Phone = customer.Phone;
                c.Email = customer.Email;
                c.Address = customer.Address;
                c.ZipCode = customer.ZipCode;
                c.City = customer.City;
            }
            return c;
        }

        public User GetUserWithOrder(string email) {
            return BuildUser(myProxy.GetUserWithOrders(email));
        }

        private User BuildUser(UserReference.User u) {
            User user = new User();
            if (u != null) {
                user.ID = u.ID;
                user.FirstName = u.FirstName;
                user.LastName = u.LastName;
                user.Phone = u.Phone;
                user.Email = u.Email;
                user.Address = u.Address;
                user.ZipCode = u.ZipCode;
                user.City = u.City;
            }
            foreach (var order in u.Orders) {
                Order o = new Order();
                o.ID = order.ID;
                user.Orders.Add(o);
            }
            return user;

        }

        public bool UpdateCustomer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city) {
            return myProxy.UpdateCustomer(firstName, lastName, phone, email, address, zipCode, city);
        }

        public bool DeleteUser(string email) {
            return myProxy.DeleteUser(email);
        }
    }
}
