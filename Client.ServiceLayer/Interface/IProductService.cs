using System.Collections.Generic;
using Client.Domain;

namespace Client.ServiceLayer {
    public interface IProductService {
        Product Create(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName);
        Product Find(int ID);
        Product FindByName(string name);
        Review FindReview(int ID);
        Product Delete(int id);
        Product Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);
        IEnumerable<Product> GetAllProducts();
        Review CreateReview(string text, int productID, int userID);
        Review DeleteReview(int reviewID, int reviewUserID);
    }
}
