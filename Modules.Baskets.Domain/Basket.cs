using Common.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Baskets.Domain;

public class Basket:BaseEntity
{
    public List<BasketItem> Items { get; set; }

    public Basket()
    {
        Items= new List<BasketItem>();
    }
}
