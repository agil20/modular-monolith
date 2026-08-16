using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modules.Categories.Contract.Services;
using Modules.Products.Contract.ProductDTOs;
using Modules.Products.Contracts.Services;
using Modules.Products.Domain;
using Modules.Products.Infrastructure.Persistence;
using MonolitModularLearning.Common.Extentions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace   Modules.Products.Controllers;


[ApiExplorerSettings(GroupName = "products")]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

private readonly ICategoryModuleService _categoryModuleService;

private readonly IProductModuleService _productModuleService;

public ProductsController( ICategoryModuleService categoryModuleService, IProductModuleService productModuleService)
{
 
    _categoryModuleService = categoryModuleService;
    _productModuleService = productModuleService;
}
  

[HttpPost]

public async Task<IActionResult> Post(RequestProductCreate productdto)
{

    await _productModuleService.Post(productdto);

    return StatusCode(201, new { message = "Product created successfully" });
}

[HttpGet]
public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int size = 10)
{
   
    var products = await _productModuleService.Get(page, size);

    return Ok(products);
}
[HttpGet("{id}")]
public async Task<IActionResult> Get(int id)
{
   var product = await _productModuleService.Get(id);
    return Ok(product);
}
[HttpPut("{id}")]
public async Task<IActionResult> Put(int id, RequestProductCreate productdto)
{
   await _productModuleService.Update(id, productdto);
    return Ok(new { message = "Product updated successfully" });
}
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
   await _productModuleService.Delete(id);
    return Ok(new { message = "Product deleted successfully" });
}

    [HttpGet("category/{categoryId}")]

    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await _productModuleService.GetProductsByCategory(categoryId);
        return Ok(products);


    }

}