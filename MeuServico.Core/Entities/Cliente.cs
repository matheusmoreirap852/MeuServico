namespace MeuServico.Core.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

        public string CNH { get; set; }
        public DateTime ValidadeCNH { get; set; }

        public ICollection<Locacao> Locacoes { get; set; }
            = new List<Locacao>();
    }
}