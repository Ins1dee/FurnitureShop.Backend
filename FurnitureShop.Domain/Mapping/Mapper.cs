using AutoMapper;
using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Dtos.DimensionsDtos;
using FurnitureShop.Domain.Dtos.FurnitureDtos;
using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Dtos.OrderProductDtos;
using FurnitureShop.Domain.Dtos.UserDtos;

namespace FurnitureShop.Domain.Mapping
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

            CreateMap<GetShoppingCartItemDto, ShoppingCartItem>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>().ReverseMap();
        }
    }
}
