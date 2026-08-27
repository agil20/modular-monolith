using Microsoft.Extensions.DependencyInjection;
using Modules.Categories.Application.Repositories;
using Modules.Categories.Application.Services;
using Modules.Categories.Contract.Services;
using Modules.Categories.Infrastructure.Repositories;

namespace Modules.Categories.Infrastructure.Extentions;

public static class CategoryExtentions
{
    public static IServiceCollection AddCategoriesModule(this IServiceCollection services)
    {
        // Servisin qeydiyyatı (Bu, çox güman ki, səndə artıq var)
        services.AddScoped<ICategoryModuleService, CategoryModuleService>();

        // ÇATIŞMAYAN SƏTİR BUDUR: Repository-nin qeydiyyatı
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}  