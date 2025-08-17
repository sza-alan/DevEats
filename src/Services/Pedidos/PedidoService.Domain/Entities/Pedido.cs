using PedidoService.Domain.Enums;

namespace PedidoService.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public Guid RestauranteId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public StatusPedido Status { get; private set; }
        public List<PedidoItem> Itens { get; private set; } = new();
        public DateTime CriadoEm { get; private set; }

        protected Pedido() { }

        public Pedido(Guid restauranteId, Guid usuarioId)
        {
            Id = Guid.NewGuid();
            RestauranteId = restauranteId;
            UsuarioId = usuarioId;
            Status = StatusPedido.Criado;
            ValorTotal = 0;
            CriadoEm = DateTime.UtcNow;
        }

        public void AdicionarItem(Guid itemId, string nomeItem, int quantidade, decimal preco)
        {
            if (preco <= 0 || quantidade <= 0)
            {
                return;
            }

            var itemExistente = Itens.FirstOrDefault(i => i.ItemId == itemId);
            if (itemExistente != null)
            {
                itemExistente.AdicionarQuantidade(quantidade);
            }
            else
            {
                Itens.Add(new PedidoItem(Id, itemId, nomeItem, quantidade, preco));
            }

            CalcularValorTotal();
        }

        public void CalcularValorTotal()
        {
            ValorTotal = Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
        }

        public void MarcarComoPago()
        {
            if (Status == StatusPedido.Criado)
            {
                Status = StatusPedido.Pago;
            }
        }
    }
}
