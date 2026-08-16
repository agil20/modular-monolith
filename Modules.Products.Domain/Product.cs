using Common.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Domain
{
    public class Product:BaseEntity
    {

       

        public string Name { get; set; }

        public double Price { get; set; }
        public int CategoryId { get; set; }

        public ProductDescription? ProductDescription { get; set; }
    }
}
