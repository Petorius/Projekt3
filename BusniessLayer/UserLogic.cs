using BusniessLayer;
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
        private AdminDB adminDB;
        private ProductDB productDB;
        private OrderLineDB orderLineDB;
        private ProductLogic productLogic;

        public UserLogic() {
            userDB = new UserDB();
            account = new Account();
            cl = new CustomerLogic();
            adminDB = new AdminDB();
            productDB = new ProductDB();
            orderLineDB = new OrderLineDB();
            productLogic = new ProductLogic();
        }

        public User GetUserWithOrders(string email) {
            
            User user = userDB.GetUserWithOrders(email);
            
            return user;
        }

        public User GetUserWithOrdersAndOrderlines(string email) {
            User user = GetUserWithOrders(email);

            foreach (Order o in user.Orders) {
                o.Orderlines = orderLineDB.GetOrderlinesByOrderID(o.ID);
                foreach(OrderLine ol in o.Orderlines) {
                    ol.Product = productLogic.GetProduct("productID", ol.Product.ID.ToString());
                }
            }
            
            return user;
        }

        // Database test constructor. Only used for testing.
        public UserLogic(string connectionString) {
            userDB = new UserDB(connectionString);
            account = new Account();
            cl = new CustomerLogic(connectionString);
        }

        // Creates an user with password. Hashes the password with auto-generated
        // salt and returns true if user was created and false otherwise.
        public User CreateUserWithPassword(string firstName, string lastName, string street,
            int zip, string city, string email, int number, string password) {

            string s = account.CreatePasswordHash(password);
            char[] splitter = { ':' };
            var split = s.Split(splitter);
            string salt = split[0];
            string hashValue = split[1];

            // Checks if customer exists in the database. If customer exists we update,
            // otherwise we create a new customer.
            Customer c = cl.HandleCustomer(firstName, lastName, street, zip, city, email, number);

            return userDB.CreateUser(c.ID, salt, hashValue);
        }

        // Validates an users attempt to login. Returns true if password matches with the email
        // and returns false otherwise.
        public User ValidatePassword(string email, string password) {
            User user = userDB.GetUser("email", email);
            if(user.ErrorMessage == "") {
                if(!account.ValidateLogin(user.Salt, user.HashPassword, password)) {
                    user.ErrorMessage = "Forkert email eller kodeord";
                }
            }
            return user;
        }

        public Admin ValidateAdminLogin(string email, string password) {
            Admin admin = adminDB.GetAdmin(email);
            if(admin.ErrorMessage == "") {
                if(!account.ValidateLogin(admin.Salt, admin.HashPassword, password)) {
                    admin.ErrorMessage = "Forkert email eller kodeord";
                }
            }
            return admin;
        }

        public Admin CreateAdminLogin(string email, string password) {
            string s = account.CreatePasswordHash(password);
            char[] splitter = { ':' };
            var split = s.Split(splitter);
            string salt = split[0];
            string hashValue = split[1];

            return adminDB.CreateAdmin(email, hashValue, salt);
        }

        public User UpdatePassword(int userID, string newpassword) {
            string s = account.CreatePasswordHash(newpassword);
            char[] splitter = { ':' };
            var split = s.Split(splitter);
            string salt = split[0];
            string hashValue = split[1];
            
            return userDB.UpdateUser(userID, salt, hashValue);
        }
    }
}
