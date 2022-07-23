using FurnitureStorage.Domain.Dtos.Furniture;
using FurnitureStorage.Domain.Models;
using FurnitureStorage.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStorage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly IFurnitureService furnitureService;

        public FurnitureController(IFurnitureService furnitureService)
        {
            this.furnitureService = furnitureService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await furnitureService.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await furnitureService.GetByIdAsync(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddFurnitureDto newFurniture)
        {
            return Ok(await furnitureService.AddAsync(newFurniture));
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateFurnitureDto updatedFurniture)
        {
            var result = await furnitureService.UpdateAsync(id, updatedFurniture);
            if(result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await furnitureService.DeleteAsync(id);

            if(result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
