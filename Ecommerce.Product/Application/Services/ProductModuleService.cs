using Ecommerce.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Product.Application.Services
{
    public class ProductModuleService : IProductModuleApi
    {
        public async Task<string> GetProductNameAsync(int productId)
        {

            return await Task.FromResult("Test Məhsulu");
        }

        public async Task<decimal> GetProductPriceAsync(int productId)
        {

          return await Task.FromResult(99.99m);            
        }
    }
}
