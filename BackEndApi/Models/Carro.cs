using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndApi.Models
{
    public class Carro
    {
        public int Id { get; set; }

        // =============================
        // IDENTIFICAÇÃO
        // =============================
        [Required, StringLength(10)]
        public string Placa { get; set; } = default!;

        [Required, StringLength(60)]
        public string Marca { get; set; } = default!;

        [Required, StringLength(80)]
        public string Modelo { get; set; } = default!;

        public int Ano { get; set; }

        [StringLength(30)]
        public string? Cor { get; set; }

        // =============================
        // EMPRESA (MULTIEMPRESA)
        // =============================
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = default!;

        // =============================
        // CONTROLE OPERACIONAL
        // =============================
        public int KmAtual { get; set; }

        public StatusCarro Status { get; set; }
            = StatusCarro.Disponivel;

        // =============================
        // FINANCEIRO
        // =============================
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPago { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorVenal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorVenda { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorSeguro { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorIPVAAnual { get; set; }

        public DateTime DataCompra { get; set; }

        // =============================
        // AUDITORIA
        // =============================
        public DateTime CriadoEm { get; set; }
            = DateTime.UtcNow;

        public DateTime AtualizadoEm { get; set; }
            = DateTime.UtcNow;

        // =============================
        // NAVEGAÇÃO
        // =============================
        public ICollection<Locacao> Locacoes { get; set; }
            = new List<Locacao>();

        public ICollection<Manutencao> Manutencoes { get; set; }
            = new List<Manutencao>();

        public ICollection<DespesaGeral> Despesas { get; set; }
            = new List<DespesaGeral>();
    }
}