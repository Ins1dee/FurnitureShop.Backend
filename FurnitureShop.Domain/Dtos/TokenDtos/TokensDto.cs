using FurnitureShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.Dtos.TokensDtos
{
    public class TokensDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public RefreshToken RefreshToken { get; set; } = new RefreshToken();
    }
}
