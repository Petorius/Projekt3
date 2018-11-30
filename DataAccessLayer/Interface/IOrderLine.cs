using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface {
    public interface IOrderLine : ICRUD<OrderLine> {

        bool CreateInDesktop(OrderLine Entity, bool test = false, bool testResult = false);

        bool DeleteInDesktop(OrderLine Entity, bool test = false, bool testResult = false);
 
    }
}
