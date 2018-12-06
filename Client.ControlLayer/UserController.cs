using Client.Domain;
using Client.ServiceLayer;

namespace Client.ControlLayer {
    public class UserController {

        private IUserService userService;
        public UserController() {
            userService = new UserService();
        }

        // Checks if the email is already registered on an user
        // and returns true if an email is registered on an user, otherwise false.
        public User IsEmailAlreadyRegistered(string email) {
            User user = userService.GetUser(email);
            if(user.ID > 0) {
                user.ErrorMessage = "Denne email er tilknyttet en bruger, venligst log ind med denne bruger";
            }
            return user;
            
        }
        
        public User GetUser(string email) {
            return userService.GetUser(email);
        }

        public User GetUserWithOrders(string email) {
            return userService.GetUserWithOrder(email);
        }

        public Customer GetCustomerByMail(string email) {
            return userService.GetCustomerByEmail(email);
        }

        public Customer UpdateCustomer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city, string existingemail) {
            return userService.UpdateCustomer(firstName, lastName, phone, email, address, zipCode, city, existingemail);
        }

      
        public User DeleteUser(string email) {
            return userService.DeleteUser(email);
        }
    }
}
