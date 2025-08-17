using System.ComponentModel.DataAnnotations;

namespace DevEats.Application.DTOs
{
    public class MenuItemCreateDTO
    {
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, 9999.99)]
        public decimal Preco { get; set; }
    }
}
