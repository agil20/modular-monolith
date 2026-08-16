using Microsoft.Extensions.DependencyInjection;
using Modules.Basket.Contract.Services;
using Modules.Baskets.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Baskets.Infrastructure.Extentions;


public static class BasketExtention
{
public static IServiceCollection AddBasketModule(this IServiceCollection services)
{
    services.AddScoped<IBasketModuleService, BasketModuleService>();
    return services;
}

}