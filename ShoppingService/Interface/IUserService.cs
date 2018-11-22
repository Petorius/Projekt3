using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IUserService {
        [OperationContract]
        bool CreateUserWithPassword(string firstName, string lastName, string street,
            int zip, string city, string email, int number, string password);
        [OperationContract]
        bool ValidatePassword(string email, string password);
    }
}
