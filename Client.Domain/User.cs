using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class User : Customer {

        public User(string firstName, string lastName, int phone, string email, string address,
            int zipCode, string city) : base(firstName, lastName, phone, email, address, zipCode, city) {

        }

        public User() {

        }

    }
}
