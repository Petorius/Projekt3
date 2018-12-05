using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface {
    public interface IOrderLine : ICRUD<OrderLine> {

        OrderLine CreateInDesktop(OrderLine Entity, bool test = false, bool testResult = false);

        OrderLine DeleteInDesktop(OrderLine Entity, bool test = false, bool testResult = false);
 
    }
}
