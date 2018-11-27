using System.Collections.Generic;
using Client.Domain;

namespace Client.ServiceLayer {
    public interface ITagService {
        Tag FindTagByName(string name);
    }
}