using Modules.Products.Application.Repositories;
using Modules.Products.Domain;
using Modules.Products.Infrastructure.Persistence;

public class ProductPriceHistoryRepository : IProductPriceHistoryRepository
{
    private readonly ProductsDbContext _context;

    public ProductPriceHistoryRepository(ProductsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductPriceHistory history)
    {
        await _context.Set<ProductPriceHistory>().AddAsync(history);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}