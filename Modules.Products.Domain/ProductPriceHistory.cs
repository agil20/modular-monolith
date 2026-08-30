using Common.Entites;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modules.Products.Domain;

public class ProductPriceHistory:BaseEntity
{

    public int ProductId { get; set; }
    public double? OldPrice { get; set; }
    public double NewPrice { get; set; }
    public DateTime ChangedDate { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}