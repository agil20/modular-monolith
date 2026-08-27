using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Application;
using Modules.Products.Application.Repositories;
using Modules.Products.Contracts.Services;
using Modules.Products.Infrastructure.Repositories;
using Modules.Products.Infrastructure.Service;

namespace Modules.Products.Infrastructure.Extentions;

public static class ProductExtentions
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services)
    {
        services.AddScoped<IProductModuleService, ProductModuleService>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}