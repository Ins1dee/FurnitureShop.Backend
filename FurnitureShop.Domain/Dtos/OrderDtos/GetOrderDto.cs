using FurnitureShop.Domain.Dtos.OrderProductDtos;

namespace FurnitureShop.Domain.Dtos.OrderDtos
{
    public class GetOrderDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<GetShoppingCartItemDto> Products { get; set; } = new List<GetShoppingCartItemDto>();

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerNumber { get; set; } = string.Empty;

        public string CustomerAddress { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public double TotalAmount { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
