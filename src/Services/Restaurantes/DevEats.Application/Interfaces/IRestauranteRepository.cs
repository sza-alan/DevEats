using DevEats.Domain.Entities;

namespace DevEats.Application.Interfaces
{
    public interface IRestauranteRepository
    {
        Task<List<Restaurante>> GetAllAsync();
        Task<Restaurante?> GetByIdAsync(Guid id);
        Task AddAsync(Restaurante restaurante);
        Task AddMenuItemAsync(MenuItem menuItem);
    }
}
