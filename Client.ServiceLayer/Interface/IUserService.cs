using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceLayer {
    public interface IUserService {

        bool CreateUserWithPassword(string firstName, string lastName, string street,
        int zip, string city, string email, int number, string password);

        bool ValidatePassword(string email, string password);

        User GetUser(string email);
    }
}
