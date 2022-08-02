using FurnitureStorage.Data.Entities;
using FurnitureStorage.Domain.Dtos.OrderDtos;
using FurnitureStorage.Domain.Dtos.OrderProductDtos;
using FurnitureStorage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<List<GetOrderDto>>> CreateAsync(CreateOrderDto newOrder);

        Task<Result<List<GetOrderDto>>> DeleteAsync(int id);

        Task<Result<GetOrderDto>> GetByIdAsync(int id);

        Task<Result<GetOrderDto>> AddProductsAsync(int orderId, params OrderProductDto[] productsDto);

        Task<Result<GetOrderDto>> RemoveProductsAsync(int orderId, params int[] productId);

        Task<Result<List<GetOrderDto>>> GetAllAsync();
    }
}
