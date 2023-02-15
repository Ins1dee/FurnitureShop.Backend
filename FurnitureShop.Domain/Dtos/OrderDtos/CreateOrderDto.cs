using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Domain.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; } = string.Empty;

        public string CustomerAddress { get; set; } = string.Empty;

        public string CustomerNumber { get; set; } = string.Empty;
    }
}
