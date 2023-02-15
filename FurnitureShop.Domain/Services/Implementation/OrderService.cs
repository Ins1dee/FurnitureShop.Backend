using AutoMapper;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Infrastructure;
using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Models;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FurnitureShop.Domain.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Furniture> furnitureRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly IMapper mapper;

        public OrderService(IRepository<Order> orderRepository, IRepository<Furniture> furnitureRepository, 
            IRepository<User> userRepository, IRepository<ShoppingCart> shoppingCartRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.furnitureRepository = furnitureRepository;
            this.userRepository = userRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetOrderDto>>> CreateAsync(CreateOrderDto newOrder, string userEmail)
        {
            var user = await userRepository
                .Query()
                .Include(u => u.RefreshTokens)
                .Include(u => u.Orders)
                .Include(u => u.ShoppingCart)
                .ThenInclude(sc => sc.Items)
                .ThenInclude(sc => sc.Furniture)
                .ThenInclude(x => x.FurnitureDimensions)
                .Where(u => u.Email.Equals(userEmail))
                .FirstOrDefaultAsync();

            if(user is null)
            {
                return Result.Failed<List<GetOrderDto>>("User not found");
            }

            if(user.ShoppingCart is null)
            {
                return Result.Failed<List<GetOrderDto>>("Your shopping cart is empty");
            }

            var orderToCreate = mapper.Map<Order>(newOrder);
            orderToCreate.UserId = user.Id;
            orderToCreate.Products = user.ShoppingCart.Items;

            await orderRepository.AddAsync(orderToCreate);
            await orderRepository.SaveChangesAsync();

            orderToCreate.Name = "Order #" + orderToCreate.Id;
            orderToCreate.TotalAmount = orderToCreate.Products.Select(x => x.Furniture).Sum(x => x.Price);
            await orderRepository.UpdateAsync(orderToCreate);
            await orderRepository.SaveChangesAsync();

            var shoppingCartToDelete = await shoppingCartRepository.GetByIdAsync(user.ShoppingCart.Id);
            shoppingCartRepository.Delete(shoppingCartToDelete);
            await shoppingCartRepository.SaveChangesAsync();

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

        public async Task<Result<List<GetOrderDto>>> GetOrdersAsync(string userEmail)
        {
            var user = await userRepository
                .Query()
                .Include(u => u.RefreshTokens)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Products)
                .ThenInclude(sc => sc.Furniture)
                .ThenInclude(f => f.FurnitureDimensions)
                .Where(u => u.Email.Equals(userEmail))
                .FirstOrDefaultAsync();

            var orders = user.Orders
                .Select(u => mapper.Map<GetOrderDto>(u))
                .ToList();

            return orders.Count() == 0 ? Result.Failed<List<GetOrderDto>>("No orders found")
               : Result.Ok(orders);
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
