using Microsoft.AspNetCore.Mvc;
using Modules.Categories.Contract.Services;
using Modules.Products.Contract.ProductDTOs;
using Modules.Products.Contracts.Services;
using Common.Models; 

namespace Modules.Products.Controllers;

[ApiExplorerSettings(GroupName = "products")]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ICategoryModuleService _categoryModuleService;
    private readonly IProductModuleService _productModuleService;

    public ProductsController(ICategoryModuleService categoryModuleService, IProductModuleService productModuleService)
    {
        _categoryModuleService = categoryModuleService;
        _productModuleService = productModuleService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestProductCreate productdto)
    {
        await _productModuleService.Post(productdto);

        return StatusCode(201, new ApiResponseModel(true, 201, "Product created successfully"));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int size = 5,string? search=null)
    {
        var products = await _productModuleService.Get(page, size,search); 

        return Ok(new ApiResponseModel(true, 200, "Products retrieved successfully", products));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productModuleService.Get(id);

        return Ok(new ApiResponseModel(true, 200, "Product retrieved successfully", product));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestProductCreate productdto)
    {
        await _productModuleService.Update(id, productdto);

        return Ok(new ApiResponseModel(true, 200, "Product updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productModuleService.Delete(id);

        return Ok(new ApiResponseModel(true, 200, "Product deleted successfully"));
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await _productModuleService.GetProductsByCategory(categoryId);

        // Uğurludur, data olaraq kateqoriyaya aid məhsulları veririk
        return Ok(new ApiResponseModel(true, 200, "Products by category retrieved successfully", products));
    }
}