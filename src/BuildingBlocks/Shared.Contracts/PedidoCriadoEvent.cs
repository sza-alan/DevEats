namespace Shared.Contracts
{
    public record PedidoCriadoEvent(
        Guid IdPedido,
        Guid IdRestaurante,
        Guid IdUsuario,
        decimal ValorTotal,
        List<ItemDoPedidoContrato> Itens
    );

    public record ItemDoPedidoContrato(
        Guid ItemId,
        string Nome,
        int Quantidade,
        decimal PrecoUnitario
    );
}
