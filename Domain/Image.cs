using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {
    [DataContract]
    public class Image {
        public int ImageID { get; set; }
        [DataMember]
        public string ImageSource { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
