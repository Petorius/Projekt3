using Client.Domain;

namespace Client.ServiceLayer {
    public class UserService : IUserService {

        UserReference.UserServiceClient myProxy;

        public UserService() {
            myProxy = new UserReference.UserServiceClient();
        }

        public User GetUser(string email) {
            var u = myProxy.GetUser(email);
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

            return user;
        }

        public Customer GetCustomerByEmail(string email) {
            var customer = myProxy.GetCustomerByMail(email);
            Customer c = new Customer();
            if (customer != null) {
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
    }
}
