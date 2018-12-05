using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Image {
        public int ImageID { get; set; }
        public string ImageSource { get; set; }
        public string Name { get; set; }
        public string ErrorMessage { get; set; }
    }
}
