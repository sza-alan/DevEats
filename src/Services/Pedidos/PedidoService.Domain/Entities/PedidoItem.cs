namespace PedidoService.Domain.Entities
{
    public class PedidoItem
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ItemId { get; private set; }
        public string NomeItem { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        protected PedidoItem() { }

        public PedidoItem(Guid pedidoId, Guid itemId, string nomeItem, int quantidade, decimal precoUnitario)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            ItemId = itemId;
            NomeItem = nomeItem;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public void AdicionarQuantidade(int quantidadeAdicional)
        {
            Quantidade += quantidadeAdicional;
        }
    }
}
