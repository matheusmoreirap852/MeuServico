using System.ComponentModel.DataAnnotations;

namespace MeuServico.Application.Dtos
{
    public class CarroDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Placa { get; set; } = default!;

        [Required]
        [StringLength(60)]
        public string Marca { get; set; } = default!;

        [Required]
        [StringLength(80)]
        public string Modelo { get; set; } = default!;

        [Range(1900, 2100)]
        public int Ano { get; set; }

        [StringLength(30)]
        public string? Cor { get; set; }

        [Range(0, int.MaxValue)]
        public int KmAtual { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ValorPago { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ValorVenal { get; set; }

        [Required]
        public int EmpresaId { get; set; }
    }
}