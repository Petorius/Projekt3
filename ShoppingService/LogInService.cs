using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.ServiceLayer.Interface;
using Server.BusinessLogic;

namespace Server.ServiceLayer {
    public class LogInService : ILogInService {
        private UserLogic userLogic;

        public LogInService() {
            userLogic = new UserLogic();
        }

        public bool CreateUserWithPassword(string firstName, string lastName, string street, int zip, string city, string email, int number, string password) {
            return userLogic.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }

        public bool ValidatePassword(string email, string password) {
            return userLogic.ValidatePassword(email, password);
        }
    }
}
