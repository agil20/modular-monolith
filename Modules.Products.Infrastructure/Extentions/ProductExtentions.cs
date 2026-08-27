using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Contracts.Services;

using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Application.Extentions;

public static class ProductExtentions
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services)
    {
        services.AddScoped<IProductModuleService, ProductModuleService>();
        return services;
    }
}