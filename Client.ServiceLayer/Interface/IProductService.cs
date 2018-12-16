using System.Collections.Generic;
using Client.Domain;

namespace Client.ServiceLayer {
    public interface IProductService {
        Product Create(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName);
        Product GetProduct(string select, string input);
        Product GetProductWithImages(string select, string input);
        Product GetProductWithReviews(string select, string input);
        Product GetProductWithImagesAndReviews(string select, string input);
        IEnumerable<Product> GetAllProductsWithImages();
        Review FindReview(int ID);
        Product Delete(int id);
        Product Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);
        Review CreateReview(string text, int productID, int userID);
        Review DeleteReview(int reviewID, int reviewUserID);
    }
}
