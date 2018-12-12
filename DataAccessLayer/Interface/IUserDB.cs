using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface {
    public interface IUserDB {
        User CreateUser(int key, string salt, string hashValue);

        User GetUser(string select, string input);

        User GetUserWithOrders(string email);
        
        User DeleteUser(string email, bool test = false, bool testResult = false);

        User UpdateUser(int userID, string salt, string hashValue, bool test = false, bool testResult = false);
    }
}
