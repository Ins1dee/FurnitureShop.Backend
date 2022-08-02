using FurnitureStorage.Data.Entities;
using FurnitureStorage.Domain.Dtos.OrderDtos;
using FurnitureStorage.Domain.Dtos.OrderProductDtos;
using FurnitureStorage.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStorage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await orderService.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await orderService.GetByIdAsync(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOrderDto newOrder)
        {
            return Ok(await orderService.CreateAsync(newOrder));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> RemoveProductsFromOrder(int id)
        {
            var result = await orderService.DeleteAsync(id);

            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("AddToOrder/{orderId}")]
        public async Task<IActionResult> AddProductsToOrder(int orderId, params OrderProductDto[] productsDto)
        {
            var result = await orderService.AddProductsAsync(orderId, productsDto);

            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("DeleteFromOrder/{orderId}")]
        public async Task<IActionResult> RemoveProductsFromOrder(int orderId, params int[] productId)
        {
            var result = await orderService.RemoveProductsAsync(orderId, productId);

            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
