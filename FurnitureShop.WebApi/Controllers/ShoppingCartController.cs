using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Dtos.OrderProductDtos;
using FurnitureShop.Domain.Dtos.UserDtos;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace FurnitureShop.WebApi.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await shoppingCartService.GetAllItems(userEmail));
        }

        [HttpPost]
        public async Task<IActionResult> AddItems([FromBody] List<ShoppingCartItemDto> items)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await shoppingCartService.AddItemsAsync(userEmail, items));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItems([FromBody] int itemId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await shoppingCartService.RemoveItemAsync(userEmail, itemId));
        }
    }
}
