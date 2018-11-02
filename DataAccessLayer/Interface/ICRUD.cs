using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public interface ICRUD<T> {
        void Create(T Entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(int id);
        void Delete(int id);
    }
}
