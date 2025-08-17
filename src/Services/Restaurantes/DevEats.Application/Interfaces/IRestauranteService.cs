using DevEats.Application.DTOs;
using PedidoService.Application.DTOs;

namespace DevEats.Application.Interfaces
{
    public interface IRestauranteService
    {
        Task<List<RestauranteViewDTO>> GetAllAsync();
        Task<Guid> CreateAsync(RestauranteCreateDTO restauranteCreateDTO);
        Task<Guid?> AddMenuItemAsync(Guid restauranteId, MenuItemCreateDTO menuItemCreateDTO);
        Task<RestauranteDetailsViewDTO?> GetByIdAsync(Guid id);
    }
}
