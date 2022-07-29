using AutoMapper;
using FurnitureStorage.Data.Entities;
using FurnitureStorage.Data.Infrastructure;
using FurnitureStorage.Domain.Dtos.Furniture;
using FurnitureStorage.Domain.Models;
using FurnitureStorage.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStorage.Domain.Services.Implementation
{
    public class FurnitureService : IFurnitureService
    {
        private readonly IRepository<Furniture> furnitureRepository;
        private readonly IMapper mapper;

        public FurnitureService(IRepository<Furniture> furnitureRepository, IMapper mapper)
        {
            this.furnitureRepository = furnitureRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetFurnitureDto>>> AddAsync(AddFurnitureDto newFurniture)
        {
            Furniture addFurniture = mapper.Map<Furniture>(newFurniture);

            //addFurniture.Id = furnitureRepository.Query().Max(f => f.Id) + 1;
            addFurniture.FurnitureDimensions = mapper.Map<Dimensions>(newFurniture.FurnitureDimensions);
            await furnitureRepository.AddAsync(addFurniture);
            await furnitureRepository.SaveChangesAsync();
            var result = furnitureRepository.Query().Select(f => mapper.Map<GetFurnitureDto>(f)).ToList();

            return Result.Ok(result);
        }

        public async Task<Result<List<GetFurnitureDto>>> GetAllAsync()
        {
            var result = await furnitureRepository
                .Query()
                .Include(f => f.FurnitureDimensions)
                .Select(f => mapper.Map<GetFurnitureDto>(f))
                .ToListAsync();

            return result.Count() == 0 ? Result.Failed<List<GetFurnitureDto>>("No products found!")
                                  : Result.Ok(result);
        }

        public async Task<Result<GetFurnitureDto>> GetByIdAsync(int id)
        {
            var result = mapper.Map<GetFurnitureDto>(await furnitureRepository.GetByIdAsync(id));

            return result is null ? Result.Failed<GetFurnitureDto>("No product with such id found!")
                                  : Result.Ok(result);

        }

        public async Task<Result<GetFurnitureDto>> UpdateAsync(UpdateFurnitureDto updatedFurniture)
        {

            var furnitureToUpdate = await furnitureRepository.GetByIdAsync(updatedFurniture.Id);

            if (furnitureToUpdate != null)
            {
                mapper.Map(updatedFurniture, furnitureToUpdate);
                await furnitureRepository.SaveChangesAsync();
            }

            var result = mapper.Map<GetFurnitureDto>(furnitureToUpdate);

            return result is null ? Result.Failed<GetFurnitureDto>("No product with such id found!")
                                  : Result.Ok(result);
        }

        public async Task<Result<List<GetFurnitureDto>>> DeleteAsync(int id)
        {
            var furnitureToDelete = await furnitureRepository.GetByIdAsync(id);

            if (furnitureToDelete is null)
            {
                return Result.Failed<List<GetFurnitureDto>>("No product with such id found!");
            }

            furnitureRepository.Delete(furnitureToDelete);
            furnitureRepository.SaveChangesAsync();
            var result = furnitureRepository.Query().Select(f => mapper.Map<GetFurnitureDto>(f)).ToList();

            return Result.Ok(result);
        }
    }
}
