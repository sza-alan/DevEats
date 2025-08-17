namespace PedidoService.Application.DTOs
{
    public class RestauranteDetailsViewDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public List<MenuItemViewDTO> Cardapio { get; set; } = new();
    }
}