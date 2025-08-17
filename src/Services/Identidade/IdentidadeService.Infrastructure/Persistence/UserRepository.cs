using IdentidadeService.Application.Interfaces;
using IdentidadeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentidadeService.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentidadeDbContext _dbContext;

        public UserRepository(IdentidadeDbContext dbContext) => _dbContext = dbContext;

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
