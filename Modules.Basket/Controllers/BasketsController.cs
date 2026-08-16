using Microsoft.AspNetCore.Mvc;
using Modules.Basket.Contract.Services;
using Modules.Baskets.Contract.DTOs.BasketItemDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return Ok(basketItems);
        }
        [HttpPost("{basketId}/items")]
        public async Task<IActionResult> AddItemToBasket(int basketId, [FromBody] RequestBasketItem requestBasketItem)
        {
            await _basketModuleService.AddItemToBasketAsync(basketId, requestBasketItem);
            return Ok();
        }

    }
}
