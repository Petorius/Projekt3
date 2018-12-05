using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface {
    public interface IOrder : ICRUD<Order> {
        Order CreateReturnID(Order Entity, bool test = false, bool testResult = false);
    }
}
