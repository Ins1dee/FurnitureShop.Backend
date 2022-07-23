using AutoMapper;
using FurnitureStorage.Domain.Dtos.Furniture;
using FurnitureStorage.Domain.Models;
using FurnitureStorage.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Services.Implementation
{
    public class FurnitureService : IFurnitureService
    {

        private static List<Furniture> furniture = new List<Furniture>
        {
            new Furniture { Id = 0, Name = "Comfort chair", FurnitureDimensions = new Dimensions(70, 12, 20) },
            new Furniture { Id = 1, Name = "Comfort bed", FurnitureDimensions = new Dimensions(80, 20, 30) }
        };

        private readonly IMapper mapper;

        public FurnitureService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<Result<List<GetFurnitureDto>>> AddAsync(AddFurnitureDto newFurniture)
        {
            Furniture addFurniture = mapper.Map<Furniture>(newFurniture);
            addFurniture.Id = furniture.Max(f => f.Id) + 1;
            furniture.Add(addFurniture);
            var result = furniture.Select(f => mapper.Map<GetFurnitureDto>(f)).ToList();

            return Result.Ok(result);
        }

        public async Task<Result<List<GetFurnitureDto>>> GetAllAsync()
        {
            var result = furniture.Select(f => mapper.Map<GetFurnitureDto>(f)).ToList();

            return result is null ? Result.Failed<List<GetFurnitureDto>>("No products found!")
                                  :  Result.Ok(result);
        }

        public async Task<Result<GetFurnitureDto>> GetByIdAsync(int id)
        {
            try
            {
                var result = mapper.Map<GetFurnitureDto>(furniture.First(f => f.Id == id));

                return Result.Ok(result);
            }
            catch(Exception ex)
            {
                return Result.Failed<GetFurnitureDto>(ex.Message);
            }
        }

        public async Task<Result<GetFurnitureDto>> UpdateAsync(int id, UpdateFurnitureDto updatedFurniture)
        {
            try
            {
                Furniture furnitureToUpdate = furniture.First(f => f.Id == id);
                mapper.Map(updatedFurniture, furnitureToUpdate);
                var result = mapper.Map<GetFurnitureDto>(furnitureToUpdate);

                return Result.Ok(result);
            }
            catch(Exception ex)
            {
                return Result.Failed<GetFurnitureDto>(ex.Message);
            }
        }

        public async Task<Result<List<GetFurnitureDto>>> DeleteAsync(int id)
        {
            try
            {
                var furnitureToDelete = furniture.First(f => f.Id == id);
                furniture.Remove(furnitureToDelete);
                var result = furniture.Select(f => mapper.Map<GetFurnitureDto>(f)).ToList();

                return Result.Ok(result);
            }
            catch(Exception ex)
            {
                return Result.Failed<List<GetFurnitureDto>>(ex.Message);
            }
        }
    }
}
