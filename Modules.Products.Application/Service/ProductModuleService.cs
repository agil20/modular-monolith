using Modules.Categories.Contract.Services;
using Modules.Products.Contract.ProductDTOs;
using Modules.Products.Contracts.Services;
using Modules.Products.Domain;
using Modules.Products.Application.Repositories;
using Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modules.Products.Contracts.ProductDTOs;

namespace Modules.Products.Application;

public class ProductModuleService : IProductModuleService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryModuleService _categoryModuleService;

    public ProductModuleService(IProductRepository productRepository, ICategoryModuleService categoryModuleService)
    {
        _productRepository = productRepository;
        _categoryModuleService = categoryModuleService;
    }

    public async Task<List<ResponseProductGet>> Get(int page, int size, string? search = null)
    {
        var products = await _productRepository.GetPagedProductsAsync(page, size, search);

        if (!products.Any())
            throw new NotFoundException("Product Tapilmadi");

        var categoryIds = products.Select(p => p.CategoryId).Distinct().ToList();
        var categoryNames = await _categoryModuleService.GetCategoryNamesAsync(categoryIds);

        var response = products.Select(p => new ResponseProductGet
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryId = p.CategoryId,
            Description = p.ProductDescription != null ? p.ProductDescription.Description : string.Empty,
            CategroyName = categoryNames.TryGetValue(p.CategoryId, out string catName) ? catName : "Tapılmadı"
        }).ToList();

        return response;
    }

    public async Task<ResponseProductGet> Get(int id)
    {
        var product = await _productRepository.GetProductWithDescriptionAsync(id, trackChanges: false);

        if (product is null)
            throw new NotFoundException("Məhsul tapılmadı");

        var categoryName = await _categoryModuleService.GetCategoryNameAsync(product.CategoryId);

        return new ResponseProductGet
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Description = product.ProductDescription != null ? product.ProductDescription.Description : string.Empty,
            CategroyName = categoryName
        };
    }

    public async Task<Dictionary<int, ResponseProductGet>> GetProductNamesByIdsAsync(List<int> productIds)
    {
        var products = await _productRepository.GetProductsByIdsAsync(productIds);

        return products.ToDictionary(p => p.Id, p => new ResponseProductGet
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryId = p.CategoryId
        });
    }

    public async Task<List<ResponseProductGet>> GetProductsByCategory(int categoryId)
    {
        var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
        var categoryName = string.Empty;

        if (products.Any())
        {
            categoryName = await _categoryModuleService.GetCategoryNameAsync(categoryId);
        }

        return products.Select(p => new ResponseProductGet
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryId = p.CategoryId,
            Description = p.ProductDescription != null ? p.ProductDescription.Description : string.Empty,
            CategroyName = categoryName
        }).ToList();
    }

    public async Task Post(RequestProductCreate productdto)
    {
        var product = new Product
        {
            Name = productdto.Name,
            Price = productdto.Price,
            CategoryId = productdto.CategoryId,
            ProductDescription = new ProductDescription
            {
                Description = productdto.Description
            }
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task Update(int id, RequestUpdateProduct productdto)
    {
        var product = await _productRepository.GetProductWithDescriptionAsync(id, trackChanges: true);

        if (product == null)
        {
            throw new NotFoundException("Məhsul tapılmadı");
        }

        product.Name = productdto.Name;
        product.Price = productdto.Price;
        product.CategoryId = productdto.CategoryId;

        if (product.ProductDescription == null)
        {
            product.ProductDescription = new ProductDescription
            {
                Description = productdto.Description
            };
        }
        else
        {
            product.ProductDescription.Description = productdto.Description;
        }

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync();
    }
}