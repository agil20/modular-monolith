using Common.Repositories;
using Modules.Products.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modules.Products.Application.Repositories;

public interface IProductRepository : IReadRepository<Product>, IWriteRepository<Product>
{
    // Səhifələmə və axtarış üçün
    Task<IReadOnlyList<Product>> GetPagedProductsAsync(int page, int size, string? search = null);

    // Məhsulu öz açıqlaması (ProductDescription) ilə birlikdə gətirmək üçün
    Task<Product?> GetProductWithDescriptionAsync(int id, bool trackChanges);

    // ID-lər siyahısına görə məhsulları çəkmək üçün
    Task<IReadOnlyList<Product>> GetProductsByIdsAsync(List<int> productIds);

    // Konkret bir kateqoriyaya aid olan məhsulları tapmaq üçün
    Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId);
}