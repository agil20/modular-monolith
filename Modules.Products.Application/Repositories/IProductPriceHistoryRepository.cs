using Modules.Products.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Application.Repositories;

public interface IProductPriceHistoryRepository
{
    Task AddAsync(ProductPriceHistory history);
    Task SaveChangesAsync();

}
