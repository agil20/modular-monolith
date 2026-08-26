using Microsoft.AspNetCore.Mvc;
using Modules.Categories.Contract.CategoryDTOs;
using Modules.Categories.Contract.Services;
using Common.Models;
using Modules.Categories.Contracts.CategoryDTOs; // Bütün cavablar artıq ApiResponseModel ilə idarə olunur

namespace Modules.Categories.Controllers;

[ApiExplorerSettings(GroupName = "categories")]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryModuleService _categoryModuleService;

    public CategoryController(ICategoryModuleService categoryModuleService)
    {
        _categoryModuleService = categoryModuleService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestCategoryCreate categorydto)
    {
        await _categoryModuleService.Post(categorydto);

        // Data yoxdur, sadəcə 201 və mesaj
        return StatusCode(201, new ApiResponseModel(true, 201, "Category created successfully"));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var responseCategories = await _categoryModuleService.Get();

        // Uğurludur, data olaraq kateqoriya siyahısını veririk
        return Ok(new ApiResponseModel(true, 200, "Categories retrieved successfully", responseCategories));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestCategoryUpdate categorydto)
    {
        await _categoryModuleService.Update(id, categorydto);

        return Ok(new ApiResponseModel(true, 200, "Category updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryModuleService.Delete(id);

        return Ok(new ApiResponseModel(true, 200, "Category deleted successfully"));
    }
}