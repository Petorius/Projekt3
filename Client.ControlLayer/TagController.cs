using System.Collections.Generic;
using System.Linq;
using Client.Domain;
using Client.ServiceLayer;

namespace Client.ControlLayer {
    public class TagController {
        private ITagService tagService;
        public TagController() {
            tagService = new TagService();
        }

        public Tag FindTagByName(string name) {
            Tag t = tagService.FindTagByName(name);
            t.Products.OrderByDescending(p => p.Sales);
            return t;
        }
    }
}

