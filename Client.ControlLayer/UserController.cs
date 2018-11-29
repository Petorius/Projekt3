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
        public bool IsEmailAlreadyRegistered(string email) {
            bool res = false;
            User user = userService.GetUser(email);
            if(user.ID > 0) {
                res = true;
            }
            return res;
        }
        
        public User GetUser(string email) {
            return userService.GetUser(email);
        }

        public Customer GetCustomerByMail(string email) {
            return userService.GetCustomerByEmail(email);
        }

        public User GetUserWithOrders(string email) {
            return userService.GetUserWithOrder(email);
        }
    }
}
