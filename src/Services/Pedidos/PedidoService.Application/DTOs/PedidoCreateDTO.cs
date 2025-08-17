namespace PedidoService.Application.DTOs
{
    public class PedidoCreateDTO
    {
        public Guid RestauranteId { get; set; }
        public Guid UsuarioId { get; set; }
        public List<PedidoItemCreateDTO> Itens { get; set; } = new();
    }
}
