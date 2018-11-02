﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Server.Domain;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface IProductService {
        [OperationContract]
        void CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description);

        [OperationContract]
        Product FindProduct(int ID);

        [OperationContract]
        void DeleteProduct(int id);

        [OperationContract]
        void Update(int ID);
    }
}
