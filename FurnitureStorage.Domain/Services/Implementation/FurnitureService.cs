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

        public async Task<ServiceResponse<List<GetFurnitureDto>>> Add(AddFurnitureDto newFurniture)
        {
            var serviceResponse = new ServiceResponse<List<GetFurnitureDto>>();
            Furniture addFurniture = mapper.Map<Furniture>(newFurniture);
            addFurniture.Id = furniture.Max(f => f.Id) + 1;
            furniture.Add(addFurniture);
            serviceResponse.Data = furniture.Select(f => mapper.Map<GetFurnitureDto>(f)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFurnitureDto>>> GetAll()
        {
            return new ServiceResponse<List<GetFurnitureDto>> 
            { 
                Data = furniture.Select(f => mapper.Map<GetFurnitureDto>(f)).ToList() 
            };
        }

        public async Task<ServiceResponse<GetFurnitureDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetFurnitureDto>();
            try
            {
                serviceResponse.Data = mapper.Map<GetFurnitureDto>(furniture.First(f => f.Id == id));
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFurnitureDto>> Update(int id, UpdateFurnitureDto updatedFurniture)
        {
            ServiceResponse<GetFurnitureDto> serviceResponse = new ServiceResponse<GetFurnitureDto>();
            try
            {
                Furniture furnitureToUpdate = furniture.First(f => f.Id == id);
                mapper.Map(updatedFurniture, furnitureToUpdate);
                serviceResponse.Data = mapper.Map<GetFurnitureDto>(furnitureToUpdate);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFurnitureDto>>> Delete(int id)
        {
            ServiceResponse<List<GetFurnitureDto>> serviceResponse = new ServiceResponse<List<GetFurnitureDto>>();
            
            try
            {
                var furnitureToDelete = furniture.First(f => f.Id == id);
                furniture.Remove(furnitureToDelete);
                serviceResponse.Data = furniture.Select(f => mapper.Map<GetFurnitureDto>(f)).ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
