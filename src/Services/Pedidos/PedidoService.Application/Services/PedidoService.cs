using MassTransit;
using PedidoService.Application.DTOs;
using PedidoService.Application.Interfaces;
using PedidoService.Domain.Entities;
using Shared.Contracts;
using System.Net.Http.Json;

namespace PedidoService.Application.Services
{
    public class RestauranteDetailsViewDTO { public Guid Id { get; set; } public List<MenuItemViewDTO> Cardapio { get; set; } = new(); }
    public class MenuItemViewDTO { public Guid Id { get; set; } public string Nome { get; set; } public decimal Preco { get; set; } }

    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPublishEndpoint _publishEndpoint;

        public PedidoService(IPedidoRepository pedidoRepository, IHttpClientFactory httpClientFactory, IPublishEndpoint publishEndpoint)
        {
            _pedidoRepository = pedidoRepository;
            _httpClientFactory = httpClientFactory;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid?> CreateAsync(PedidoCreateDTO pedidoCreateDTO)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var restauranteUrl = $"https://localhost:7036/api/restaurantes/{pedidoCreateDTO.RestauranteId}";

            RestauranteDetailsViewDTO? restauranteData;
            try
            {
                restauranteData = await httpClient.GetFromJsonAsync<RestauranteDetailsViewDTO>(restauranteUrl);
            }
            catch (HttpRequestException)
            {
                return null;
            }

            if (restauranteData == null) return null;

            var pedido = new Pedido(pedidoCreateDTO.RestauranteId, pedidoCreateDTO.UsuarioId);

            foreach (var itemDto in pedidoCreateDTO.Itens)
            {
                var menuItem = restauranteData.Cardapio.FirstOrDefault(c => c.Id == itemDto.ItemId);
                if (menuItem == null) return null;

                pedido.AdicionarItem(itemDto.ItemId, menuItem.Nome, itemDto.Quantidade, menuItem.Preco);
            }

            await _pedidoRepository.AddAsync(pedido);

            var itensContrato = pedido.Itens
                .Select(item => new ItemDoPedidoContrato(item.ItemId, item.NomeItem, item.Quantidade, item.PrecoUnitario))
                .ToList();

            var evento = new PedidoCriadoEvent(
                pedido.Id,
                pedido.RestauranteId,
                pedido.UsuarioId,
                pedido.ValorTotal,
                itensContrato
            );

            await _publishEndpoint.Publish(evento);

            return pedido.Id;
        }
    }
}
