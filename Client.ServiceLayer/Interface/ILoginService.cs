using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer.Interface {
    public interface ILoginService {
        User CreateUserWithPassword(string firstName, string lastName, string street,
        int zip, string city, string email, int number, string password);
        User ValidatePassword(string email, string password);
        Admin ValidateAdminLogin(string email, string password);
        User UpdatePassword(int userID, string newpassword);
    }
}
