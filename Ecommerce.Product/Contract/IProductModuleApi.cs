namespace Ecommerce.Product.Contracts
{
    public interface IProductModuleApi
    {
        Task<decimal> GetProductPriceAsync(int productId);
        Task<string> GetProductNameAsync(int productId);
    }
}