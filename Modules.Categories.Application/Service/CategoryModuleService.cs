using Modules.Categories.Contract.CategoryDTOs;
using Modules.Categories.Contract.Services;
using Modules.Categories.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;
using Modules.Categories.Contracts.CategoryDTOs;
using Modules.Categories.Application.Repositories;

namespace Modules.Categories.Application.Services;

public class CategoryModuleService : ICategoryModuleService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryModuleService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<ResponseCategory>> Get()
    {
        var categories = await _categoryRepository.GetAllAsync();

        var responseCategories = categories.Select(c => new ResponseCategory
        {
            Name = c.Name,
            Id = c.Id,
        }).ToList();

        if (!responseCategories.Any())
            throw new NotFoundException("Sistemdə heç bir kateqoriya tapılmadı");

        return responseCategories;
    }

    public async Task<string> GetCategoryNameAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new NotFoundException("Kateqoriya tapılmadı");

        return category.Name;
    }

    public async Task<Dictionary<int, string>> GetCategoryNamesAsync(List<int> ids)
    {
        var allCategories = await _categoryRepository.GetAllAsync();

        return allCategories
            .Where(c => ids.Contains(c.Id))
            .ToDictionary(c => c.Id, c => c.Name);
    }

    public async Task Post(RequestCategoryCreate requestCategoryCreate)
    {
        var category = new Category
        {
            Name = requestCategoryCreate.Name
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task Update(int id, RequestCategoryUpdate responseCategory)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new NotFoundException($"ID-si {id} olan kateqoriya tapılmadı");

        category.Name = responseCategory.Name;

        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new NotFoundException($"ID-si {id} olan kateqoriya silinmək üçün tapılmadı");

        _categoryRepository.Remove(category);
        await _categoryRepository.SaveChangesAsync();
    }
}