using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.BusinessLogic;
using Server.Domain;

namespace Server.ServiceLayer {
    public class LoginService : ILoginService {
        private UserLogic userLogic;

        public LoginService() {
            userLogic = new UserLogic();
        }

        public User CreateUserWithPassword(string firstName, string lastName, string street, int zip, string city, string email, int number, string password) {
            return userLogic.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }

        public Admin ValidateAdminLogin(string email, string password) {
            return userLogic.ValidateAdminLogin(email, password);
        }

        public User ValidatePassword(string email, string password) {
            return userLogic.ValidatePassword(email, password);
        }
    }
}
