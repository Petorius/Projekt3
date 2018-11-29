using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface {
    public interface IUserDB {
        bool CreateUser(int key, string salt, string hashValue);
        User GetUser(string email);

        User GetUserWithOrders(string email);

        bool DeleteUser(string email, bool test = false, bool testResult = false);
    }
}
