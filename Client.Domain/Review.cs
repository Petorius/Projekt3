using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain
{
    public class Review
    {
        public string _Title { get; set; }
        public string _Description { get; set; }
        public Copy _Copy { get; set; }
    }
}
