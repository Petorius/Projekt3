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
        public int ID { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }

        public Review(string text) {
            Text = text;
            DateCreated = DateTime.Today;
        }

        public Review() {
            DateCreated = DateTime.Today;
        }


    }
}
