using Common.Entites;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Modules.Baskets.Domain
{
    public class BasketItem:BaseEntity
    {
        public int BasketId { get; set; }
        public Basket  Basket { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
