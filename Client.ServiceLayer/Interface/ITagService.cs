using System.Collections.Generic;
using Client.Domain;

namespace Client.ServiceLayer {
    public interface ITagService {
        Tag FindTagByName(string name);

        Category GetSalesByCategory(string name);

        IEnumerable<Product> GetAllSales();

        Tag FindBestSellers(string name);

        IEnumerable<Product> GetBestsellersInCategory(string name);
    }
}