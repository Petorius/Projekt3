using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Server.Domain {
    [DataContract]
    public class Tag {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Product> Products { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }

        public Tag() {
            Products = new List<Product>();
            ErrorMessage = "";
        }


    }
}
