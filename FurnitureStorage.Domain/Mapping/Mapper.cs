using AutoMapper;
using FurnitureStorage.Data.Entities;
using FurnitureStorage.Domain.Dtos.DimensionsDtos;
using FurnitureStorage.Domain.Dtos.FurnitureDtos;
using FurnitureStorage.Domain.Dtos.OrderDtos;
using FurnitureStorage.Domain.Dtos.OrderProductDtos;
using FurnitureStorage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<GetFurnitureDto, Furniture>().ReverseMap();
            CreateMap<AddFurnitureDto, Furniture>().ReverseMap();
            CreateMap<UpdateFurnitureDto, Furniture>().ReverseMap();

            CreateMap<DimensionsDto, Dimensions>().ReverseMap();

            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<GetOrderDto, Order>().ReverseMap();

            CreateMap<GetOrderProductDto, OrderProduct>().ReverseMap();
        }
    }
}
