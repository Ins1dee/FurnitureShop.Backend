using FurnitureShop.Domain.Dtos.FurnitureDtos;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.WebApi.Controllers
{
    [Route("api/furniture")]
    [ApiController]
    [Authorize]
    public class FurnitureController : ControllerBase
    {
        private readonly IFurnitureService furnitureService;

        public FurnitureController(IFurnitureService furnitureService)
        {
            this.furnitureService = furnitureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await furnitureService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await furnitureService.GetByIdAsync(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddFurnitureDto newFurniture)
        {
            return Ok(await furnitureService.AddAsync(newFurniture));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFurnitureDto updatedFurniture)
        {
            var result = await furnitureService.UpdateAsync(updatedFurniture);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await furnitureService.DeleteAsync(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
