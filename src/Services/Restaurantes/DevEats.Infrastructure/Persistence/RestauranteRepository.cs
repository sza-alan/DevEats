using DevEats.Application.Interfaces;
using DevEats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevEats.Infrastructure.Persistence
{
    public class RestauranteRepository : IRestauranteRepository
    {
        private readonly DevEatsDbContext _dbContext;

        public RestauranteRepository(DevEatsDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Restaurante>> GetAllAsync()
        {
            return await _dbContext.Restaurantes.ToListAsync();
        }

        public async Task<Restaurante?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Restaurantes
                .Include(r => r.Cardapio)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Restaurante restaurante)
        {
            await _dbContext.Restaurantes.AddAsync(restaurante);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            await _dbContext.MenuItems.AddAsync(menuItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
