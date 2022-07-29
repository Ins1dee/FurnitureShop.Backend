using AutoMapper;
using FurnitureStorage.Data.Entities;
using FurnitureStorage.Domain.Dtos.DimensionsDtos;
using FurnitureStorage.Domain.Dtos.Furniture;
using FurnitureStorage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Mapping
{
    public class FurnitureMapper : Profile
    {
        public FurnitureMapper()
        {
            CreateMap<Furniture, GetFurnitureDto>().ReverseMap();
            CreateMap<AddFurnitureDto, Furniture>().ReverseMap();
            CreateMap<UpdateFurnitureDto, Furniture>().ReverseMap();

            CreateMap<DimensionsDto, Dimensions>();
            CreateMap<Dimensions, DimensionsDto>();
        }
    }
}
