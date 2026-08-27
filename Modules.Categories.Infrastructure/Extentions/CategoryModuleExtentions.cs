using Microsoft.Extensions.DependencyInjection;
using Modules.Categories.Application.Services;
using Modules.Categories.Contract.Services;
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
