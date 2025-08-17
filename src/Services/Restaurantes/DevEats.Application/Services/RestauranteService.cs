using DevEats.Application.DTOs;
using DevEats.Application.Interfaces;
using DevEats.Domain.Entities;
using PedidoService.Application.DTOs;

namespace DevEats.Application.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly IRestauranteRepository _repository;

        public RestauranteService(IRestauranteRepository repository) => _repository = repository;

        public async Task<Guid> CreateAsync(RestauranteCreateDTO restauranteCreateDTO)
        {
            var restaurante = new Restaurante(
                restauranteCreateDTO.Nome,
                restauranteCreateDTO.Cnpj
            );

            await _repository.AddAsync(restaurante);

            return restaurante.Id;
        }

        public async Task<List<RestauranteViewDTO>> GetAllAsync()
        {
            var restaurantes = await _repository.GetAllAsync();

            var restaurantesView = restaurantes.Select(r => new RestauranteViewDTO
            {
                Id = r.Id,
                Nome = r.Nome
            }).ToList();

            return restaurantesView;
        }

        public async Task<Guid?> AddMenuItemAsync(Guid restauranteId, MenuItemCreateDTO menuItemCreateDTO)
        {
            var restaurante = await _repository.GetByIdAsync(restauranteId);
            if (restaurante == null)
                return null;

            var menuItem = new MenuItem(
                menuItemCreateDTO.Nome,
                menuItemCreateDTO.Descricao,
                menuItemCreateDTO.Preco,
                restauranteId
            );

            await _repository.AddMenuItemAsync(menuItem);

            return menuItem.Id;
        }

        public async Task<RestauranteDetailsViewDTO?> GetByIdAsync(Guid id)
        {
            var restaurante = await _repository.GetByIdAsync(id);
            if (restaurante == null ) return null;

            var restauranteDetails = new RestauranteDetailsViewDTO
            {
                Id = restaurante.Id,
                Nome = restaurante.Nome,
                Cnpj = restaurante.Cnpj,
                Cardapio = restaurante.Cardapio.Select(mi => new MenuItemViewDTO
                {
                    Id = mi.Id,
                    Nome = mi.Nome,
                    Descricao = mi.Descricao,
                    Preco = mi.Preco
                }).ToList()
            };

            return restauranteDetails;
        }
    }
}
