﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    public interface ICRUD<T> {
        void Create(T Entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T Enitity);
        void Delete(T Entity);
    }
}