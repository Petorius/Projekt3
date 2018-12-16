using System.Collections.Generic;
using Server.Domain;
using Server.DataAccessLayer;
using BusniessLayer;

namespace Server.ServiceLayer {
    public class TagService : ITagService {
        private TagLogic tagLogic;

        public TagService() {
            tagLogic = new TagLogic();
        }

        // Gets a tag with products and images
        public Tag FindTagByName(string name) {
            return tagLogic.GetTagWithProducts(name);
        }
    }
}
