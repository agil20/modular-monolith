using Microsoft.EntityFrameworkCore;
using Modules.Basket.Contract.Services;
using Modules.Baskets.Contract.DTOs.BasketItemDTOs;
using Modules.Baskets.Domain;
using Modules.Baskets.Infrastructure.Persistence;

using Modules.Products.Contracts.Services;

using System;
using System.Collections.Generic;
using System.Text;


namespace Modules.Baskets.Infrastructure.Service;

public class BasketModuleService : IBasketModuleService
{

    private readonly BasketDbContext _basketDbContext;
    private readonly IProductModuleService _productModuleService;

    public BasketModuleService(BasketDbContext basketdbcontext, IProductModuleService productModuleService)
    {
        _basketDbContext= basketdbcontext;
        _productModuleService = productModuleService;
    }
    public async Task AddItemToBasketAsync(int basketId,RequestBasketItem requestBasketItem)
    {
        var basket = await _basketDbContext.Baskets
        .Include(b => b.Items)
        .FirstOrDefaultAsync(b => b.Id == basketId);
        if (basket == null)
        { 
            basket=new Domain.Basket();
            _basketDbContext.Add(basket);
        }

        var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == requestBasketItem.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += requestBasketItem.Quantity;
      
        }
        else
        {
            basket.Items.Add(new BasketItem
            {
                ProductId = requestBasketItem.ProductId,
                Quantity = requestBasketItem.Quantity,
            });


        }

         await _basketDbContext.SaveChangesAsync();


    }

    public async Task<List<BasketItemDtos>> GetBasketAsync(int basketId)
    {
        var basket = await _basketDbContext.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.Id == basketId);

        if (basket == null) throw new InvalidOperationException("Basket not found");

        var ids=basket.Items.Select(i => i.ProductId).ToList();
        if (!ids.Any())
        {
            return new List<BasketItemDtos>();
        }

        var producttNames = await _productModuleService.GetProductNamesByIdsAsync(ids);

        var basketitemsdto = basket.Items.Select(i => new BasketItemDtos
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            Price = producttNames[i.ProductId].Price,
            ProductName = producttNames[i.ProductId].Name
        })  .ToList();

        return basketitemsdto;

    }
}
