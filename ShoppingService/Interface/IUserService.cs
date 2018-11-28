using Server.Domain;
using System.ServiceModel;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IUserService {

        [OperationContract]
        bool CreateUserWithPassword(string firstName, string lastName, string street,
            int zip, string city, string email, int number, string password);

        [OperationContract]
        bool ValidatePassword(string email, string password);

        [OperationContract]
        User GetUser(string email);

        [OperationContract]
        Customer GetCustomerByMail(string email);

    }
}
