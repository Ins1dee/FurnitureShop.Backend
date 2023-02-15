using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Data.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string PasswordSalt { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public ShoppingCart ShoppingCart { get; set; }
    }
}
