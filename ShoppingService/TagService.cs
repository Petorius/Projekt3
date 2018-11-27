using System.Collections.Generic;
using Server.Domain;
using Server.DataAccessLayer;

namespace Server.ServiceLayer {
    public class TagService : ITagService {
        private TagDB tagDB;

        public TagService() {
            tagDB = new TagDB();
        }

        public Tag FindTagByName(string name) {
            return tagDB.Get(name);
        }
    }
}
