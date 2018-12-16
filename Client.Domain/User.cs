using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class User : Customer {
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }

        public User(string firstName, string lastName, int phone, string email, string address,
            int zipCode, string city) : base(firstName, lastName, phone, email, address, zipCode, city) {
            Orders = new List<Order>();
            Reviews = new List<Review>();
            ErrorMessage = "";
        }

        public User() {
            Orders = new List<Order>();
            Reviews = new List<Review>();
            ErrorMessage = "";
        }

        public override string ToString() {
            return ""+ID;
        }
    }
}
