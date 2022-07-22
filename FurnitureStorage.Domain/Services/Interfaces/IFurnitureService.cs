using FurnitureStorage.Domain.Dtos.Furniture;
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
        Task<ServiceResponse<List<GetFurnitureDto>>> GetAll();

        Task<ServiceResponse<GetFurnitureDto>> GetById(int id);

        Task<ServiceResponse<List<GetFurnitureDto>>> Add(AddFurnitureDto newFurniture);

        Task<ServiceResponse<GetFurnitureDto>> Update(int id, UpdateFurnitureDto updatedFurniture);

        Task<ServiceResponse<List<GetFurnitureDto>>> Delete(int id);
    }
}
