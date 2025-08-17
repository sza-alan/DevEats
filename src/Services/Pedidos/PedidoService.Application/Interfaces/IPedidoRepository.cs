using PedidoService.Domain.Entities;

namespace PedidoService.Application.Interfaces
{
    public interface IPedidoRepository
    {
        Task AddAsync(Pedido pedido);
    }
}
