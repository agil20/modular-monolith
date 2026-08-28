using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Contracts.Services;
using Modules.Products.Application.Repositories;
using Modules.Products.Infrastructure.Repositories;

namespace Modules.Products.Application.Extentions;

public static class ProductExtentions
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services)
    {
        
        services.AddScoped<IProductModuleService, ProductModuleService>();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}