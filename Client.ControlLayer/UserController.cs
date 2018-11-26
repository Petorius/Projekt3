using Client.Domain;
using Client.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ControlLayer {
    public class UserController {

        private IUserService userService;
        public UserController() {
            userService = new UserService();
        }

        public bool CreateUserWithPassword(string firstName, string lastName, string street, int zip,
                                                string city, string email, int number, string password) {
            return userService.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }

        public bool ValidatePassword(string email, string password) {
            return userService.ValidatePassword(email, password);
        }

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
    }
}
