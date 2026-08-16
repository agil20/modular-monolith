using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Baskets.Contract.DTOs.BasketItemDTOs
{
    public class RequestBasketItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
