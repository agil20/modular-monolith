using Microsoft.AspNetCore.Mvc;
using Modules.Basket.Contract.Services;
using Modules.Baskets.Contract.DTOs.BasketItemDTOs;
using Common.Models; // Standart API cavablarımız üçün

namespace Modules.Baskets.Controllers
{
    [ApiExplorerSettings(GroupName = "baskets")]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketModuleService _basketModuleService;

        public BasketsController(IBasketModuleService basketModuleService)
        {
            _basketModuleService = basketModuleService;
        }

        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasket(int basketId)
        {
            var basketItems = await _basketModuleService.GetBasketAsync(basketId);

     
            return Ok(new ApiResponseModel(true, 200, "Basket retrieved successfully", basketItems));
        }

        [HttpPost("{basketId}/items")]
        public async Task<IActionResult> AddItemToBasket(int basketId, [FromBody] RequestBasketItem requestBasketItem)
        {
            await _basketModuleService.AddItemToBasketAsync(basketId, requestBasketItem);

            return Ok(new ApiResponseModel(true, 200, "Item added to basket successfully"));
        }


        [HttpDelete("{basketId}/items/{productId}")]
        public async Task<IActionResult> RemoveItemFromBasket(int basketId, int productId)
        {
   
            await _basketModuleService.RemoveItemFromBasketAsync(basketId, productId);


            return Ok(new ApiResponseModel(true, 200, "Məhsul səbətdən uğurla silindi"));
        }
    }
}