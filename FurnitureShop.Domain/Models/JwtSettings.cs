using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.Models
{
    public class JwtSettings
    {
        public string AccessTokenKey { get; set; } = string.Empty;

        public string RefreshTokenKey { get; set; } = string.Empty;

        public double AccessTokenExpirationTime { get; set; }

        public double RefreshTokenExpirationTime { get; set; }

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;
    }
}
