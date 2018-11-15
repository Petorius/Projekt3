using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public interface ICRUD<T> {
        bool Create(T Entity, bool test = false, bool testResult = false);
        T Get(int id);
        IEnumerable<T> GetAll();
        bool Update(T Entity, bool test = false, bool testResult = false);
        bool Delete(int id, bool test = false, bool testResult = false);
    }
}
