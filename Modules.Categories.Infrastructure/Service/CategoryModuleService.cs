using Common.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Categories.Contract.CategoryDTOs;
using Modules.Categories.Contract.Services;
using Modules.Categories.Domain;
using Modules.Categories.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions; // Xüsusi (Custom) xətalarımız üçün!

namespace Modules.Categories.Infrastructure.Service;

public class CategoryModuleService : ICategoryModuleService
{
    private readonly CategoriesDbContext _context;
    private readonly IMediator _mediator;

    public CategoryModuleService(CategoriesDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<List<ResponseCategory>> Get()
    {
        var responseCategories = await _context.Categories
            .AsNoTracking()
            .Select(c => new ResponseCategory
            {
                Name = c.Name,
                Id = c.Id,
            })
            .ToListAsync();

        // İstəyə bağlı olaraq siyahı boşdursa Front-end-ə 404 ata bilərik
        if (!responseCategories.Any())
            throw new NotFoundException("Sistemdə heç bir kateqoriya tapılmadı");

        return responseCategories;
    }

    public async Task<string> GetCategoryNameAsync(int id)
    {
        string categoryname = await _context.Categories
            .Where(c => c.Id == id)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        return categoryname;
    }

    public async Task<Dictionary<int, string>> GetCategoryNamesAsync(List<int> ids)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .Select(c => new { c.Id, c.Name })
            .ToDictionaryAsync(c => c.Id, c => c.Name);

        return categories;
    }

    public async Task Post(RequestCategoryCreate requestCategoryCreate)
    {
   

        var category = new Category
        {
            Name = requestCategoryCreate.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, RequestCategoryCreate categorydto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            // InvalidOperationException əvəzinə NotFoundException!
            throw new NotFoundException($"ID-si {id} olan kateqoriya tapılmadı");
        }

        category.Name = categorydto.Name;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            throw new NotFoundException($"ID-si {id} olan kateqoriya silinmək üçün tapılmadı");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        await _mediator.Publish(new CategoryDeleteEvent(id)); 
    }
}