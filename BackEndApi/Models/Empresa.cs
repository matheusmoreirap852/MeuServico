using System.ComponentModel.DataAnnotations;

namespace BackEndApi.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string NomeFantasia { get; set; } = default!;

        [StringLength(150)]
        public string? RazaoSocial { get; set; }

        [StringLength(18)]
        public string? Cnpj { get; set; }

        [StringLength(200)]
        public string? Endereco { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        public ICollection<Carro> Carros { get; set; } = new List<Carro>();
    }
}
