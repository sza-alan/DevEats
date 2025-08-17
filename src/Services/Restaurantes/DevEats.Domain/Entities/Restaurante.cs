namespace DevEats.Domain.Entities
{
    public class Restaurante
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public bool Ativo { get; private set; }
        public List<MenuItem> Cardapio { get; private set; } = new();

        protected Restaurante() { }

        public Restaurante(string nome, string cnpj)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Cnpj = cnpj;
            Ativo = true;
            Cardapio = new List<MenuItem>();
        }

        public void AdicionarItemCardapio(MenuItem item)
        {
            Cardapio.Add(item);
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
