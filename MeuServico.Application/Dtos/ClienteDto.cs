namespace MeuServico.Application.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

        public string CNH { get; set; }
        public DateTime ValidadeCNH { get; set; }
    }
}
