using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Domain.Dtos.UserDtos
{
    public class UserForRegistrationDto
    {
        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
