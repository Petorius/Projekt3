﻿using DataAccessLayer.Interface;
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

        public UserLogic() {
            userDB = new UserDB();
            account = new Account();
            cl = new CustomerLogic();
            adminDB = new AdminDB();
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
        public bool ValidatePassword(string email, string password) {
            bool res = false;
            User user = userDB.GetUser(email);
            if(user.ID > 0) {
                res = account.ValidateLogin(user.Salt, user.HashPassword, password);
            }
            return res;
        }

        public bool ValidateAdminLogin(string email, string password) {
            bool res = false;
            Admin admin = adminDB.GetAdmin(email);
            if(admin.ID > 0) {
                res = account.ValidateLogin(admin.Salt, admin.HashPassword, password);
            }
            return res;
        }

        public Admin CreateAdminLogin(string email, string password) {
            string s = account.CreatePasswordHash(password);
            char[] splitter = { ':' };
            var split = s.Split(splitter);
            string salt = split[0];
            string hashValue = split[1];

            return adminDB.CreateAdmin(email, hashValue, salt);
        }

    }
}
