using IdentidadeService.Domain.Entities;

namespace IdentidadeService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
