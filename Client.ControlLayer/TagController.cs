using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;
using Client.ServiceLayer;

namespace Client.ControlLayer {
    public class TagController {
        private ITagService tagService;
        public TagController() {
            tagService = new TagService();
        }

        public Tag FindTagByName(string name) {
            return tagService.FindTagByName(name);
        }

        public Category GetSalesByCategory(string name) {
            return tagService.GetSalesByCategory(name);
        }

        public Tag FindBestSellers(string name) {
            return tagService.FindTagByName("Bestseller");
        }

        public IEnumerable<Product> GetBestsellersInCategory(string name) {
            return tagService.GetBestsellersInCategory(name);
        }



    }
}

