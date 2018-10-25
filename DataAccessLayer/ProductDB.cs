using System;
using System.Collections.Generic;
using Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    class ProductDB : ICRUD<Product> {
        public void Create(Product Entity) {
            throw new NotImplementedException();
        }

        public void Delete(Product Entity) {
            throw new NotImplementedException();
        }

        public Product Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Product Enitity) {
            throw new NotImplementedException();
        }
    }
}
