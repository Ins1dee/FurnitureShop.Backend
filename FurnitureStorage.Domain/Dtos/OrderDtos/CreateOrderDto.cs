using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public string CustomerNumber { get; set; } = string.Empty;

        public string CustomerAddress { get; set; } = string.Empty;
    }
}
