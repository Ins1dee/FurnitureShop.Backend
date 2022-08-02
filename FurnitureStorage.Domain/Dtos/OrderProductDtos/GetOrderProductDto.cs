using FurnitureStorage.Domain.Dtos.FurnitureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Dtos.OrderProductDtos
{
    public class GetOrderProductDto
    {
        public GetFurnitureDto Furniture { get; set; } = new GetFurnitureDto();

        public int Quantity { get; set; }
    }
}
