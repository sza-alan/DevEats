using PedidoService.Application.DTOs;

namespace PedidoService.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<Guid?> CreateAsync(PedidoCreateDTO pedidoCreateDTO);
    }
}
