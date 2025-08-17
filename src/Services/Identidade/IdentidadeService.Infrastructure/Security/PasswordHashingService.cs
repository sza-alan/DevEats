using IdentidadeService.Application.Interfaces;

namespace IdentidadeService.Infrastructure.Security
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
