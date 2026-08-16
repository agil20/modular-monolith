using Modules.Baskets.Contract.DTOs.BasketItemDTOs;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;


namespace Modules.Basket.Contract.Services;

public interface IBasketModuleService
{
   
    Task<List<BasketItemDtos>> GetBasketAsync(int basketId);

  
    Task AddItemToBasketAsync(int basketId,RequestBasketItem requestBasketItem);


}
