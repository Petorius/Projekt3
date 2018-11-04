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
        bool Update(int id, string name, decimal price, int stock, int minStock, int maxStock, string description);
        bool Delete(int id);
    }
}
