using Server.Domain;
using System.ServiceModel;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IUserService {

        [OperationContract]
        User GetUser(string email);

        [OperationContract]
        User GetUserWithOrders(string email);

        [OperationContract]
        Customer GetCustomerByMail(string email);

        [OperationContract]
        Customer UpdateCustomer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city);

        [OperationContract]
        User DeleteUser(string email);
    }
}
