using System.Collections.Generic;
using System.ServiceModel;
using Server.Domain;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface ITagService {
        [OperationContract]
        Tag FindTagByName(string name);
    }
}
