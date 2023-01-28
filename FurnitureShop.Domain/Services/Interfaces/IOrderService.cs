using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Models;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<List<GetOrderDto>>> CreateAsync(CreateOrderDto newOrder);

        Task<Result<List<GetOrderDto>>> DeleteAsync(int id);

        Task<Result<GetOrderDto>> GetByIdAsync(int id);

        Task<Result<List<GetOrderDto>>> GetAllAsync();
    }
}
