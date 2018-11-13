﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain;

namespace Client.ServiceLayer
{
    public class TagService : ITagService
    {
        public Tag FindTagByName(string name)
        {
            ServiceReference2.TagServiceClient myProxy = new ServiceReference2.TagServiceClient();
            var t = myProxy.FindTagByName(name);
            Tag tag = new Tag();
            if (t != null)
            {
                foreach(var p in t.Products)
                {
                    Product product = new Product();
                    product.ID = p.ID;

                    tag.Products.Add(product);
                }
                return tag;
            }
            return null;
        }
    }
}