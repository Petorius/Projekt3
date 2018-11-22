using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {
    public class User : Customer {
        public string HashPassword { get; set; }
        public string Salt { get; set; }

        public User(string firstName, string lastName, int phone, string email, string address, 
            int zipCode, string city ) : base(firstName, lastName, phone, email, address, zipCode,city) {
            
        }

        public User() {

        }
    }
}
