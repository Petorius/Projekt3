using Server.Domain;
using System.ServiceModel;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IUserService {

        [OperationContract]
        User GetUser(string email);

        [OperationContract]
        Customer GetCustomerByMail(string email);
    }
}
