using FurnitureStorage.Domain.Dtos.FurnitureDtos;
using FurnitureStorage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Services.Interfaces
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
