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

        public User GetUser(string email) {
            return userDB.GetUser(email);
        }

        public User GetUserWithOrders(string email) {
            return userDB.GetUserWithOrders(email);
        }

        public Customer GetCustomerByMail(string email) {
            return customerLogic.GetCustomerByMail(email);
        }

        public Customer UpdateCustomer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city, string existingemail) {
            return customerLogic.UpdateCustomer(firstName, lastName, phone, email, address, zipCode, city, existingemail);
        }

        public User DeleteUser(string email) {
            return userDB.DeleteUser(email);
        }
    }
}
