using AutoMapper;
using FurnitureStorage.Data.Entities;
using FurnitureStorage.Data.Infrastructure;
using FurnitureStorage.Domain.Dtos.OrderDtos;
using FurnitureStorage.Domain.Dtos.OrderProductDtos;
using FurnitureStorage.Domain.Models;
using FurnitureStorage.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Services.Implementation
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

        public async Task<Result<GetOrderDto>> AddProductsAsync(int orderId, params OrderProductDto[] productsDto)
        {
            var products = new List<OrderProduct>();
            foreach (var productDto in productsDto)
            {
                var furniture = await furnitureRepository
                .Query()
                .Include(f => f.FurnitureDimensions)
                .Where(f => f.Id == productDto.FurnitureId)
                .FirstOrDefaultAsync();

                if (furniture is null)
                {
                    return Result.Failed<GetOrderDto>($"No product with id \"{productDto.FurnitureId}\" found!");
                }

                products.Add(new OrderProduct()
                {
                    Furniture = furniture,
                    Quantity = productDto.Quantity
                });
            }
            
            var orderToUpdate = await orderRepository
                .Query()
                .Include(o => o.Products)
                .ThenInclude(op => op.Furniture)
                .ThenInclude(f => f.FurnitureDimensions)
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            if(orderToUpdate is null)
            {
                return Result.Failed<GetOrderDto>($"No order with id \"{orderId}\" found!");
            } 
            
            orderToUpdate.Products.AddRange(products);

            await orderRepository.UpdateAsync(orderToUpdate);
            await orderRepository.SaveChangesAsync();

            var result = mapper.Map<GetOrderDto>(orderToUpdate);

            return Result.Ok(result);
        }

        public async Task<Result<List<GetOrderDto>>> CreateAsync(CreateOrderDto newOrder)
        {
            var orderToCreate = mapper.Map<Order>(newOrder);
            orderToCreate.Name = "Order #" + orderToCreate.Id;

            await orderRepository.AddAsync(orderToCreate);
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
            
            if(orderToDelete is null)
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

        public async Task<Result<GetOrderDto>> RemoveProductsAsync(int orderId, params int[] productId)
        {
            var orderToUpdate = await orderRepository
                .Query()
                .Include(o => o.Products)
                .ThenInclude(op => op.Furniture)
                .ThenInclude(f => f.FurnitureDimensions)
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            if (orderToUpdate is null)
            {
                return Result.Failed<GetOrderDto>($"No order with id \"{orderId}\" found!");
            }

            foreach (var id in productId)
            {
                var orderProductToRemove = orderToUpdate
                    .Products
                    .FirstOrDefault(p => p.Id == id);

                if (orderProductToRemove is null)
                {
                    return Result.Failed<GetOrderDto>($"No product with id \"{id}\" found!");
                }

                orderToUpdate.Products.Remove(orderProductToRemove);
            }

            await orderRepository.UpdateAsync(orderToUpdate);
            await orderRepository.SaveChangesAsync();

            var result = mapper.Map<GetOrderDto>(orderToUpdate);

            return Result.Ok(result);
        }
    }
}
