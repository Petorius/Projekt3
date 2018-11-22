using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;

namespace DataAccessLayer.Interface {
    public interface ICustomer {
        Customer GetByMail(string email);
        int CreateReturnedID(Customer Entity);
    }
}
