using System.ComponentModel.DataAnnotations;

namespace MeuServico.Application.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string CNH { get; set; }

        [Display(Name = "Validade CNH")]
        public DateTime ValidadeCNH { get; set; }
    }
}
