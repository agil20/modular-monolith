using Microsoft.EntityFrameworkCore;
using Modules.Categories.Application.Repositories;
using Modules.Categories.Domain;
using Modules.Categories.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modules.Categories.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CategoriesDbContext _context;
    private readonly DbSet<Category> _dbSet;

    public CategoryRepository(CategoriesDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Category>();
    }

    public async Task AddAsync(Category entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<IReadOnlyList<Category>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Remove(Category entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(Category entity)
    {
        _dbSet.Update(entity);
    }
}