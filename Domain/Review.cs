using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {

    [DataContract]
    public class Review {
        [DataMember]
        public int ReviewID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ReviewerEmail { get; set; }

        public Review(string name, string decription, int productID, string reviewerEmail) {
            Name = name;
            Description = decription;
            ProductID = productID;
            ReviewerEmail = reviewerEmail;
        }

        public Review() {

        }


    }
}
