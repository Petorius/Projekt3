using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.DataAccessLayer {
    public interface IReview {
        bool CreateReview(Review review, int productID, int userID, bool test = false, bool testResult = false);
        bool DeleteReview(Review review, bool test = false, bool testResult = false);
    }
}
