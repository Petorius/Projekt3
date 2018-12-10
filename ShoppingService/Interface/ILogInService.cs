using Server.Domain;
using System.ServiceModel;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface ILoginService {
        [OperationContract]
        User CreateUserWithPassword(string firstName, string lastName, string street,
                        int zip, string city, string email, int number, string password);

        [OperationContract]
        User ValidatePassword(string email, string password);
        [OperationContract]
        Admin ValidateAdminLogin(string email, string password);

        [OperationContract]
        User UpdatePassword(int userID, string newpassword);
    }
}
