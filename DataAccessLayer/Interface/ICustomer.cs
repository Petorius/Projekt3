using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DataAccessLayer;
using Server.Domain;

namespace DataAccessLayer.Interface {
    public interface ICustomer : ICRUD<Customer> {
        Customer GetByMail(string email);
        Customer CreateReturnedID(Customer Entity);
    }
}
