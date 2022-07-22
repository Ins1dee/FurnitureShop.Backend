using AutoMapper;
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
            CreateMap<Furniture, GetFurnitureDto>();
            CreateMap<AddFurnitureDto, Furniture>();
            CreateMap<UpdateFurnitureDto, Furniture>();
        }
    }
}
