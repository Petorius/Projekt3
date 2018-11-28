using System.ServiceModel;

namespace Server.ServiceLayer.Interface {
    public interface ILogInService {
        [OperationContract]
        bool CreateUserWithPassword(string firstName, string lastName, string street,
                        int zip, string city, string email, int number, string password);

        [OperationContract]
        bool ValidatePassword(string email, string password);
    }
}
