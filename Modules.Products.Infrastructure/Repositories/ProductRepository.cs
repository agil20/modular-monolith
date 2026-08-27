using Microsoft.EntityFrameworkCore;
using Modules.Products.Domain;
using Modules.Products.Application.Repositories;
using MonolitModularLearning.Common.Extentions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modules.Products.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Product> _dbSet;

    public ProductRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
    }

    // --- TƏMƏL METODLAR (IRepository-dən gələnlər) ---

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(Product entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(Product entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(Product entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

  

    public async Task<List<Product>> GetPagedProductsAsync(int page, int size, string? search)
    {
        var query = _dbSet.AsNoTracking().Include(p => p.ProductDescription).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
        }

        return await query.OrderBy(p => p.Id).ToPaged(page, size).ToListAsync();
    }

    public async Task<Product?> GetProductWithDescriptionAsync(int id, bool trackChanges = false)
    {
        var query = _dbSet.Include(p => p.ProductDescription).AsQueryable();

        if (!trackChanges)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetProductsByIdsAsync(List<int> ids)
    {
        return await _dbSet.AsNoTracking().Where(p => ids.Contains(p.Id)).ToListAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _dbSet.AsNoTracking()
            .Where(c => c.CategoryId == categoryId)
            .Include(p => p.ProductDescription)
            .ToListAsync();
    }
}