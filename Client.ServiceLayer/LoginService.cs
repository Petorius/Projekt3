using Client.Domain;
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
            myProxy = new LoginReference.LoginServiceClient();
        }

        public User ValidatePassword(string email, string password) {
            return BuildUser(myProxy.ValidatePassword(email, password));
        }

        public Admin ValidateAdminLogin(string email, string password) {
            return BuildClientAdmin(myProxy.ValidateAdminLogin(email, password));
        }

        public User CreateUserWithPassword(string firstName, string lastName, string street, int zip,
                                        string city, string email, int number, string password) {
            return BuildUser(myProxy.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password));
        }

        private User BuildUser(LoginReference.User u) {
            User user = new User();
            user.ID = u.ID;
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Phone = u.Phone;
            user.Email = u.Email;
            user.Address = u.Address;
            user.ZipCode = u.ZipCode;
            user.City = u.City;
            user.ErrorMessage = u.ErrorMessage;

            foreach (var order in u.Orders) {
                Order o = new Order();
                o.ID = order.ID;
                o.ErrorMessage = order.ErrorMessage;
                user.Orders.Add(o);

            }
            return user;
        }

        private Admin BuildClientAdmin(LoginReference.Admin a) {
            Admin admin = new Admin();
            admin.ErrorMessage = a.ErrorMessage;
            return admin;
        }
    }
}
