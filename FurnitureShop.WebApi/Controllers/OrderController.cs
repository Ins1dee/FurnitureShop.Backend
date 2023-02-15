using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FurnitureShop.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await orderService.GetOrdersAsync(User.FindFirstValue(ClaimTypes.Email)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await orderService.GetByIdAsync(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto newOrder)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await orderService.CreateAsync(newOrder, userEmail));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProductsFromOrder([FromQuery] int id)
        {
            var result = await orderService.DeleteAsync(id);

            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
