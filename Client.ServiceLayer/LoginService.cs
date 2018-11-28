using Client.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer {
    public class LoginService : ILoginService{
        LoginReference.LoginServiceClient myProxy;

        public LoginService() {
            myProxy = new UserReference.UserServiceClient();
        }

        public bool ValidatePassword(string email, string password) {
            return myProxy.ValidatePassword(email, password);
        }

        public bool ValidateAdminLogin(string email, string password) {
            return myProxy.ValidateAdminLogin(email, password);
        }

        public bool CreateUserWithPassword(string firstName, string lastName, string street, int zip,
                                        string city, string email, int number, string password) {
            return myProxy.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }
    }
}
