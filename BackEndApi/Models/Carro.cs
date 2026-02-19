using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndApi.Models
{
    public class Carro
    {
        public int Id { get; set; }

        [Required, StringLength(10)]
        public string Placa { get; set; } = default!;
        public decimal? ValorSeguro { get; set; }
        public decimal? ValorIPVAAnual { get; set; }


        [Required, StringLength(60)]
        public string Marca { get; set; } = default!;

        [Required, StringLength(80)]
        public string Modelo { get; set; } = default!;

        public int Ano { get; set; }

        [StringLength(30)]
        public string? Cor { get; set; }

        public int KmAtual { get; set; }

        // 🔹 Financeiro
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPago { get; set; }   // Valor que ele pagou no carro

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorVenal { get; set; }  // Valor FIPE / mercado

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorVenda { get; set; } // Caso ele venda futuramente

        public DateTime DataCompra { get; set; }

       // public StatusCarro Status { get; set; } = StatusCarro.Disponivel;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

        // Navegação
        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
        public ICollection<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();
        public ICollection<DespesaGeral> Despesas { get; set; } = new List<DespesaGeral>();
    }
}
