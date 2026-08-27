using Common.Repositories;
using Modules.Products.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Application.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetPagedProductsAsync(int page, int size, string? search);
    Task<Product?> GetProductWithDescriptionAsync(int id, bool trackChanges = false);
    Task<List<Product>> GetProductsByIdsAsync(List<int> ids);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);

}
