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
        bool DeleteProduct(int id);

        [OperationContract]
        bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);

        [OperationContract]
        IEnumerable<Product> GetAllProducts();
    }
}
