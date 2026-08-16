using Ecommerce.Product.Application.Services;
using Ecommerce.Product.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Product.Extentions
{
    public static class ProductModuleExtensions
    {
        public static IServiceCollection AddProductModule(this IServiceCollection services)
        {
            services.AddScoped<IProductModuleApi, ProductModuleService>();
            return services;
        }
    }
}
