using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Review {
        public int ReviewID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductID { get; set; }
        public string ReviewerEmail { get; set; }
    }
}
