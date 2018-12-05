using Client.Domain;
using Client.ServiceLayer;
using Client.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ControlLayer {
    public class AdminController {
        private ILoginService loginService;
        public AdminController() {
            loginService = new LoginService();
        }

        public Admin ValidateAdminLogin(string email, string password) {
            return loginService.ValidateAdminLogin(email, password);
        }

        public User CreateUserWithPassword(string firstName, string lastName, string street, int zip,
                                        string city, string email, int number, string password) {
            return loginService.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }

        public User ValidatePassword(string email, string password) {
            return loginService.ValidatePassword(email, password);
        }
    }
}
