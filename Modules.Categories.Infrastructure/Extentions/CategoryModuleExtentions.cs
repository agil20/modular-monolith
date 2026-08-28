using Microsoft.Extensions.DependencyInjection;
using Modules.Categories.Contract.Services;
using Modules.Categories.Infrastructure.Service;
using Modules.Categories.Application.Repositories;
using Modules.Categories.Infrastructure.Repositories;

namespace Modules.Categories.Extentions;

public static class CategoryModuleExtentions
{
    public static IServiceCollection AddCategoriesModule(this IServiceCollection services)
    {
        // Servisin qeydiyyatı
        services.AddScoped<ICategoryModuleService, CategoryModuleService>();


        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}