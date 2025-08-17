using PedidoService.Application.Interfaces;
using PedidoService.Domain.Entities;

namespace PedidoService.Infrastructure.Persistence
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidoServiceDbContext _context;

        public PedidoRepository(PedidoServiceDbContext context) => _context = context;

        public async Task AddAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
