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
    }
}
