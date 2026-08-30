using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events;

public class ProductPriceChangedEvent
{
    public int Id { get; set; }
   
    public double OldPrice { get; set; }
    public double NewPrice { get; set; }
}
