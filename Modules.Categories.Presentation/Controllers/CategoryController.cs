using Microsoft.AspNetCore.Mvc;

using Modules.Categories.Contract.CategoryDTOs;
using Modules.Categories.Contract.Services;



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

    // POST api/<CategoryController>
    [HttpPost]
    public async Task<IActionResult> Post(RequestCategoryCreate categorydto)
    {
                await _categoryModuleService.Post(categorydto);
        return StatusCode(201, new { message = "Category created successfully" });

    }
    [HttpGet]
    public  async Task<IActionResult>  Get()
    {
      

 var responseCategories=   await _categoryModuleService.Get();

        // Return the list of categories as a response
        return   Ok(responseCategories); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestCategoryCreate categorydto)
    {
      await _categoryModuleService.Update(id, categorydto);
        return Ok(new { message = "Category updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      
        await _categoryModuleService.Delete(id);
        return Ok(new { message = "Category deleted successfully" } );
        

    }
}