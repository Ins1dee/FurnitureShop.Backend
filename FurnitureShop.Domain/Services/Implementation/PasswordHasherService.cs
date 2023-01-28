using FurnitureShop.Domain.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FurnitureShop.Domain.Services.Implementation
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public void Hash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToHexString(hmac.Key);
                passwordHash = Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public bool Verify(string password, string passwordHash, string passwordSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromHexString(passwordSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(Convert.FromHexString(passwordHash));
            }
        }
    }
}
