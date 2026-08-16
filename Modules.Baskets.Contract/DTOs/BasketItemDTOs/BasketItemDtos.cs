using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Baskets.Contract.DTOs.BasketItemDTOs
{
    public class BasketItemDtos
    {
        public int ProductId { get; set; }     // Məhsulun ID-si
        public int Quantity { get; set; }      // Miqdarı (Say∀)
        public double Price { get; set; }     // Bir ədədinin qiyməti

        public double TotalPrice => Price * Quantity; //

        public string ProductName { get; set; }


    }
}
