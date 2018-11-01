﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer
{
    public interface IProductService {
        void Create(string name, double price, int stock, int minStock, int maxStock, string description);

        Product Find(int ID);
    }


}
