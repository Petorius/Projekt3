using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer {
    public class UserService : IUserService {
        public bool CreateUserWithPassword(string firstName, string lastName, string street, int zip, 
                                                string city, string email, int number, string password) {
            UserReference.UserServiceClient myProxy = new UserReference.UserServiceClient();
            return myProxy.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }

        public bool ValidatePassword(string email, string password) {
            UserReference.UserServiceClient myProxy = new UserReference.UserServiceClient();
            return myProxy.ValidatePassword(email, password);
        }
    }
}
