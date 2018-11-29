using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain {
    public class Admin {
        public int ID { get; set; }
        public string EmployeeEmail { get; set; }
        public string HashPassword { get; set; }
        public string Salt { get; set; }

        public Admin() {

        }
    }
}
