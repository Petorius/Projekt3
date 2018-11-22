using DataAccessLayer.Interface;
using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLogic {
    public class UserLogic {
        private IUserDB userDB;
        private Account account;
        private CustomerLogic cl;

        public UserLogic() {
            userDB = new UserDB();
            account = new Account();
            cl = new CustomerLogic();
        }

        // Database test constructor. Only used for unit testing.
        public UserLogic(string connectionString) {
            userDB = new UserDB(connectionString);
            account = new Account();
            cl = new CustomerLogic(connectionString);
        }

        public bool CreateUserWithPassword(string firstName, string lastName, string street,
            int zip, string city, string email, int number, string password) {
            string s = account.CreatePasswordHash(password);
            char[] splitter = { ':' };
            var split = s.Split(splitter);
            string salt = split[0];
            string hashValue = split[1];

            Customer c = cl.HandleCustomer(firstName, lastName, street, zip, city, email, number);
            return userDB.CreateUser(c.ID, salt, hashValue);
        }

        public bool ValidatePassword(string email, string password) {
            User user = userDB.GetUser(email);
            return account.ValidateUserLogin(user, password);
        }
    }
}
