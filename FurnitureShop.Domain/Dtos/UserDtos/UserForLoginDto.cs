using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Domain.Dtos.UserDtos
{
    public class UserForLoginDto
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
