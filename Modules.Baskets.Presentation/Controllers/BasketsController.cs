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

            // Uğurlu cavab və data (səbətin içindəkilər)
            return Ok(new ApiResponseModel(true, 200, "Basket retrieved successfully", basketItems));
        }

        [HttpPost("{basketId}/items")]
        public async Task<IActionResult> AddItemToBasket(int basketId, [FromBody] RequestBasketItem requestBasketItem)
        {
            await _basketModuleService.AddItemToBasketAsync(basketId, requestBasketItem);

            // Sadəcə təsdiq mesajı (data yoxdur)
            return Ok(new ApiResponseModel(true, 200, "Item added to basket successfully"));
        }
    }
}