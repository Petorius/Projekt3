using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain {
    public class Review {
        public string Text { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }

        public Review(string text, User user) {
            Text = text;
            User = user;
            DateCreated = DateTime.Today;
        }

        public Review() {
            DateCreated = DateTime.Today;
        }
    }
}
