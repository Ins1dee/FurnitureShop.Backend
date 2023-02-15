using FurnitureShop.Domain.Dtos.FurnitureDtos;
using FurnitureShop.Domain.Models;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IFurnitureService
    {
        public Task<Result<List<GetFurnitureDto>>> GetAllAsync();

        public Task<Result<GetFurnitureDto>> GetByIdAsync(int id);

        public Task<Result<List<GetFurnitureDto>>> AddAsync(AddFurnitureDto newFurniture);

        public Task<Result<GetFurnitureDto>> UpdateAsync(UpdateFurnitureDto updatedFurniture);

        public Task<Result<List<GetFurnitureDto>>> DeleteAsync(int id);
    }
}
