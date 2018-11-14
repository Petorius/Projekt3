using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;
using Server.DataAccessLayer;

namespace Server.ServiceLayer {
    public class TagService : ITagService {
        TagDB tagDB;

        public TagService() {
            tagDB = new TagDB();
        }

        public Tag FindTagByName(string name) {
            return tagDB.Get(name);
        }
    }
}
