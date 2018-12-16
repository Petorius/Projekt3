using System.Collections.Generic;
using System.ServiceModel;
using Server.Domain;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IProductService {

        [OperationContract]
        Product CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName);

        [OperationContract]
        Review FindReview(int ID);

        [OperationContract]
        Product GetProduct(string select, string input);

        [OperationContract]
        Product GetProductWithImages(string select, string input);

        [OperationContract]
        Product GetProductWithReviews(string select, string input);

        [OperationContract]
        Product GetProductWithImagesAndReviews(string select, string input);

        [OperationContract]
        Product DeleteProduct(int id);

        [OperationContract]
        Product Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);

        [OperationContract]
        IEnumerable<Product> GetAllProductsWithImages();

        [OperationContract]
        Review CreateReview(string text, int productID, int userID);

        [OperationContract]
        Review DeleteReview(int reviewID, int reviewUserID);
    }
}
