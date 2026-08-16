using Common.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Domain
{
    public class ProductDescription:BaseEntity
    {

        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
