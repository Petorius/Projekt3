﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer {
    public class UserService : IUserService {
        public bool CreateUserWithPassword(string firstName, string lastName, string street, int zip, 
                                                string city, string email, int number, string password) {
            UserReference.UserServiceClient myProxy = new UserReference.UserServiceClient();
            return myProxy.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
        }

        public User GetUser(string email) {
            UserReference.UserServiceClient myProxy = new UserReference.UserServiceClient();
            var u = myProxy.GetUser(email);
            User user = new User();
            if (u != null) {
                user.ID = u.ID;
                user.FirstName = u.FirstName;
                user.LastName = u.LastName;
                user.Phone = u.Phone;
                user.Email = u.Email;
                user.Address = u.Address;
                user.ZipCode = u.ZipCode;
                user.City = u.City;
            }
            return user;
        }

        public bool ValidatePassword(string email, string password) {
            UserReference.UserServiceClient myProxy = new UserReference.UserServiceClient();
            return myProxy.ValidatePassword(email, password);
        }
    }
}