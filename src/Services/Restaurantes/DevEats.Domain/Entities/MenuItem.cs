namespace DevEats.Domain.Entities
{
    public class MenuItem
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public Guid RestauranteId { get; private set; }

        public Restaurante Restaurante { get; private set; }

        public MenuItem(string nome, string descricao, decimal preco, Guid restauranteId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            RestauranteId = restauranteId;
        }
    }
}
