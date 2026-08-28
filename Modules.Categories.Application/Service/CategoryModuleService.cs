using Common.Exceptions;
using Modules.Categories.Application.Repositories;
using Modules.Categories.Contract.CategoryDTOs;
using Modules.Categories.Contract.Services;
using Modules.Categories.Contracts.CategoryDTOs;
using Modules.Categories.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modules.Categories.Infrastructure.Service;

public class CategoryModuleService : ICategoryModuleService
{
    // DbContext əvəzinə artıq Repository istifadə edirik
    private readonly ICategoryRepository _repository;

    public CategoryModuleService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ResponseCategory>> Get()
    {
        var categories = await _repository.GetAllAsync();

        var responseCategories = categories
            .Select(c => new ResponseCategory
            {
                Name = c.Name,
                Id = c.Id,
            })
            .ToList();

        if (!responseCategories.Any())
            throw new NotFoundException("Sistemdə heç bir kateqoriya tapılmadı");

        return responseCategories;
    }

    public async Task<string> GetCategoryNameAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        // Null gələrsə xəta verməməsi üçün yoxlama
        return category?.Name ?? string.Empty;
    }

    public async Task<Dictionary<int, string>> GetCategoryNamesAsync(List<int> ids)
    {
        var categories = await _repository.GetAllAsync();

        return categories
            .Where(c => ids.Contains(c.Id))
            .ToDictionary(c => c.Id, c => c.Name);
    }

    public async Task Post(RequestCategoryCreate requestCategoryCreate)
    {
        var category = new Category
        {
            Name = requestCategoryCreate.Name
        };

        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
    }

    public async Task Update(int id, RequestCategoryUpdate responseCategory)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            throw new NotFoundException($"ID-si {id} olan kateqoriya tapılmadı");
        }

        category.Name = responseCategory.Name;

        _repository.Update(category);
        await _repository.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            throw new NotFoundException($"ID-si {id} olan kateqoriya silinmək üçün tapılmadı");
        }

        _repository.Remove(category);
        await _repository.SaveChangesAsync();
    }
}