using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Contracts.Services;
using Modules.Products.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Infrastructure.Extentions
{
    public static class ProductExtentions
    {
        public static IServiceCollection AddProductsModule(this IServiceCollection services)
        {
            services.AddScoped<IProductModuleService, PrductModuleService>();
            return services;
        }
    }
}