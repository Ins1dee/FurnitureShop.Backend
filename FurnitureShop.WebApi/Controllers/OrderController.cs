using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
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
            return Ok(await orderService.GetAllAsync());
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
        public async Task<IActionResult> Create([FromBody] CreateOrderDto newOrder)
        {
            return Ok(await orderService.CreateAsync(newOrder));
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
