using FurnitureStorage.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Dtos.OrderProductDtos
{
    public class OrderProductDto
    {
        public int FurnitureId { get; set; }

        public int Quantity { get; set; }
    }
}
