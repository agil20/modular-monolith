using System;

namespace Modules.Products.Domain;

public class ProductPriceHistory
{
    public int Id { get; set; }
  
    public decimal? OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangedDate { get; set; }
    public Product Product { get; set; }
}