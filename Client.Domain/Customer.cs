using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Customer {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

        public Customer(string firstName, string lastName, int phone, string email, string address, int zipCode, string city) {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
            ZipCode = zipCode;
            City = city;
        }

        public Customer() {

        }
    }
}
