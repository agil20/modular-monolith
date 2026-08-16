using Microsoft.Extensions.DependencyInjection;
using Modules.Categories.Contract.Services;
using Modules.Categories.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Categories.Extentions
{
    public static class CategoryModuleExtentions
    {
        public  static IServiceCollection AddCategoriesModule(this IServiceCollection services)
        {
            services.AddScoped<ICategoryModuleService, CategoryModuleService>();
            return services;
        }
    }
}
