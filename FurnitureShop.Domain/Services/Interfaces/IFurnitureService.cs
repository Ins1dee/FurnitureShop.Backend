using FurnitureShop.Domain.Dtos.FurnitureDtos;
using FurnitureShop.Domain.Models;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IFurnitureService
    {
        Task<Result<List<GetFurnitureDto>>> GetAllAsync();

        Task<Result<GetFurnitureDto>> GetByIdAsync(int id);

        Task<Result<List<GetFurnitureDto>>> AddAsync(AddFurnitureDto newFurniture);

        Task<Result<GetFurnitureDto>> UpdateAsync(UpdateFurnitureDto updatedFurniture);

        Task<Result<List<GetFurnitureDto>>> DeleteAsync(int id);
    }
}
