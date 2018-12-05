using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.DataAccessLayer {
    public interface ICRUD<T> {
        T Create(T Entity, bool test = false, bool testResult = false);
        T Get(int id);
        IEnumerable<T> GetAll();
        T Update(T Entity, bool test = false, bool testResult = false);
        T Delete(T Entity, bool test = false, bool testResult = false);
    }
}
