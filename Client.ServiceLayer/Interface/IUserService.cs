using Client.Domain;

namespace Client.ServiceLayer {
    public interface IUserService {
        User GetUser(string email);
        User GetUserWithOrder(string email);
        Customer GetCustomerByEmail(string email);
        bool UpdateCustomer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city);
    }
}
