using System.Collections.Generic;
using System.ServiceModel;
using Server.Domain;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IProductService {

        [OperationContract]
        bool CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName);

        [OperationContract]
        Product FindProduct(int ID);

        [OperationContract]
        Review FindReview(int ID);

        [OperationContract]
        Product FindProductByName(string name);

        [OperationContract]
        bool DeleteProduct(int id);

        [OperationContract]
        bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);

        [OperationContract]
        IEnumerable<Product> GetAllProducts();

        [OperationContract]
        bool CreateReview(string text, int productID, int userID);

        [OperationContract]
        bool DeleteReview(int reviewID, int reviewUserID);
    }
}
