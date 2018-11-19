using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.DataAccessLayer {
    public class OrderDB : ICRUD<Order> {
        public bool Create(Order Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }

        public bool Delete(int id, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }

        public Order Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll() {
            throw new NotImplementedException();
        }

        public bool Update(Order Entity, bool test = false, bool testResult = false) {
            throw new NotImplementedException();
        }
    }
}
