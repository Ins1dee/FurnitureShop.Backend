namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IPasswordHasherService
    {
        public void Hash(string password, out string passwordHash, out string passwordSalt);

        public bool Verify(string password, string passwordHash, string passwordSalt);
    }
}
