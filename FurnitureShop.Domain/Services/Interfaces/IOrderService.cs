using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Models;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Result<List<GetOrderDto>>> CreateAsync(CreateOrderDto newOrder, string userEmail);

        public Task<Result<List<GetOrderDto>>> DeleteAsync(int id);

        public Task<Result<GetOrderDto>> GetByIdAsync(int id);

        public Task<Result<List<GetOrderDto>>> GetOrdersAsync(string userEmail);
    }
}
