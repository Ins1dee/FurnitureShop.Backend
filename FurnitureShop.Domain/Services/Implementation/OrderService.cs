using AutoMapper;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Infrastructure;
using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Models;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Domain.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Furniture> furnitureRepository;
        private readonly IMapper mapper;

        public OrderService(IRepository<Order> orderRepository, IRepository<Furniture> furnitureRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.furnitureRepository = furnitureRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetOrderDto>>> CreateAsync(CreateOrderDto newOrder)
        {
            var orderToCreate = mapper.Map<Order>(newOrder);

            await orderRepository.AddAsync(orderToCreate);
            await orderRepository.SaveChangesAsync();
            orderToCreate.Name = "Order #" + orderToCreate.Id;
            await orderRepository.UpdateAsync(orderToCreate);
            await orderRepository.SaveChangesAsync();

            var result = await orderRepository
                .Query()
                .Include(o => o.Products)
                .ThenInclude(op => op.Furniture)
                .ThenInclude(f => f.FurnitureDimensions)
                .Select(o => mapper.Map<GetOrderDto>(o))
                .ToListAsync();

            return Result.Ok(result);
        }

        public async Task<Result<List<GetOrderDto>>> DeleteAsync(int id)
        {
            var orderToDelete = await orderRepository.GetByIdAsync(id);

            if (orderToDelete is null)
            {
                return Result.Failed<List<GetOrderDto>>($"No order with id \"{id}\" found!");
            }

            orderRepository.Delete(orderToDelete);
            await orderRepository.SaveChangesAsync();

            var result = await orderRepository
                .Query()
                .Include(o => o.Products)
                .ThenInclude(op => op.Furniture)
                .ThenInclude(f => f.FurnitureDimensions)
                .Select(o => mapper.Map<GetOrderDto>(o))
                .ToListAsync();

            return Result.Ok(result);
        }

        public async Task<Result<List<GetOrderDto>>> GetAllAsync()
        {
            var result = await orderRepository
                .Query()
                .Include(o => o.Products)
                .ThenInclude(op => op.Furniture)
                .ThenInclude(f => f.FurnitureDimensions)
                .Select(o => mapper.Map<GetOrderDto>(o))
                .ToListAsync();

            return result.Count() == 0 ? Result.Failed<List<GetOrderDto>>("No products found!")
                : Result.Ok(result);
        }

        public async Task<Result<GetOrderDto>> GetByIdAsync(int id)
        {
            var result = await orderRepository
                .Query()
                .Include(o => o.Products)
                .ThenInclude(op => op.Furniture)
                .ThenInclude(f => f.FurnitureDimensions)
                .Where(o => o.Id == id)
                .Select(o => mapper.Map<GetOrderDto>(o))
                .FirstOrDefaultAsync();

            return result is null ? Result.Failed<GetOrderDto>($"No product with id \"{id}\" found!")
                : Result.Ok(result);
        }
    }
}
