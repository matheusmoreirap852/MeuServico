using System.ComponentModel.DataAnnotations;

namespace BackEndApi.Models
{
    public class RegistroServico
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Anotacoes { get; set; }
        public DateTime? DataCadastro { get; set; } = DateTime.Now;
    }
}
