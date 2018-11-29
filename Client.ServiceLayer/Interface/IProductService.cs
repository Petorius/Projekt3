using System.Collections.Generic;
using Client.Domain;

namespace Client.ServiceLayer {
    public interface IProductService {
        bool Create(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName);

        Product Find(int ID);

        Product FindByName(string name);

        bool Delete(int id);

        bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);

        IEnumerable<Product> GetAllProducts();

        bool CreateReview(string text, int productID, int userID);
    }
}
