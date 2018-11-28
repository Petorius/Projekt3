using Client.Domain;

namespace Client.ServiceLayer {
    public interface IUserService {
        User GetUser(string email);
        Customer GetCustomerByEmail(string email);
    }
}
