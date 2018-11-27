using DataAccessLayer.Interface;
using Server.BusinessLogic;
using Server.DataAccessLayer;
using Server.Domain;

namespace Server.ServiceLayer {
    public class UserService : IUserService {
        private UserLogic userLogic;
        private IUserDB userDB;
        private CustomerLogic customerLogic;

        public UserService() {
            userLogic = new UserLogic();
            userDB = new UserDB();
            customerLogic = new CustomerLogic();
        }
        public bool CreateUserWithPassword(string firstName, string lastName, string street,
                        int zip, string city, string email, int number, string password) {
            return userLogic.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }

        public User GetUser(string email) {
            return userDB.GetUser(email);
        }

        public bool ValidatePassword(string email, string password) {
            return userLogic.ValidatePassword(email, password);
        }

        public Customer GetCustomerByMail(string email) {
            return customerLogic.GetCustomerByMail(email);
        }
    }
}
