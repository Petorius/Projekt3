using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using Server.Domain;

namespace Server.ServiceLayer
{
    [ServiceContract]
    public interface ITagService
    {
        [OperationContract]
        Tag FindTagByName(string name);

        [OperationContract]
        Category GetSalesByCategory(string name);

        [OperationContract]
        IEnumerable<Product> GetAllSales();

        [OperationContract]
        Tag FindBestSellers(string name);

    }
}
