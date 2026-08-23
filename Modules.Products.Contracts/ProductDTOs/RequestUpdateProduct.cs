using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Contracts.ProductDTOs
{
    public class RequestUpdateProduct
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
