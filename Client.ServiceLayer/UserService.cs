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
            return BuildCustomer(myProxy.GetCustomerByMail(email));
        }

        public User GetUserWithOrder(string email) {
            return BuildUser(myProxy.GetUserWithOrders(email));
        }

        private User BuildUser(UserReference.User u) {
            User user = new User();
            user.ID = u.ID;
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Phone = u.Phone;
            user.Email = u.Email;
            user.Address = u.Address;
            user.ZipCode = u.ZipCode;
            user.City = u.City;
            user.ErrorMessage = u.ErrorMessage;

            foreach (var order in u.Orders) {
                Order o = new Order();
                o.ID = order.ID;
                o.ErrorMessage = order.ErrorMessage;
                user.Orders.Add(o);

            }
            return user;
        }

        public Customer UpdateCustomer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city, string existingemail) {
            return BuildCustomer(myProxy.UpdateCustomer(firstName, lastName, phone, email, address, zipCode, city, existingemail));
        }

        public User DeleteUser(string email) {
            return BuildUser(myProxy.DeleteUser(email));
        }

        private Customer BuildCustomer(UserReference.Customer c) {
            Customer customer = new Customer();
            customer.ID = c.ID;
            customer.FirstName = c.FirstName;
            customer.LastName = c.LastName;
            customer.Phone = c.Phone;
            customer.Email = c.Email;
            customer.Address = c.Address;
            customer.ZipCode = c.ZipCode;
            customer.City = c.City;
            customer.ErrorMessage = c.ErrorMessage;
            return customer;
        }
    }
}
