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
        public async Task<ActionResult<ServiceResponse<List<GetFurnitureDto>>>> GetAll()
        {
            return Ok(await furnitureService.GetAll());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServiceResponse<GetFurnitureDto>>> GetById(int id)
        {
            var serviceResponse = await furnitureService.GetById(id);
            if (serviceResponse.Data is null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<GetFurnitureDto>>>> Add(AddFurnitureDto newFurniture)
        {
            return Ok(await furnitureService.Add(newFurniture));
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ServiceResponse<GetFurnitureDto>>> Update(int id, UpdateFurnitureDto updatedFurniture)
        {
            var serviceResponse = await furnitureService.Update(id, updatedFurniture);
            if(serviceResponse.Data is null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<GetFurnitureDto>>> Delete(int id)
        {
            var serviceResponse = await furnitureService.Delete(id);

            if(serviceResponse.Data is null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}
