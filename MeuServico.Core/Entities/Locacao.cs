using MeuServico.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuServico.Core.Entities
{
    public class Locacao
    {
        public int Id { get; set; }

        // ===============================
        // RELACIONAMENTOS
        // ===============================
        public int CarroId { get; set; }
        public Carro Carro { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // ===============================
        // PERÍODO
        // ===============================
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }

        // ===============================
        // KM
        // ===============================
        public int KmInicial { get; set; }
        public int? KmFinal { get; set; }

        // ===============================
        // VALORES
        // ===============================
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorDiaria { get; set; }

        public int QuantidadeDiarias { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotalPrevisto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorTotalFinal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MultaAtraso { get; set; }

        // ===============================
        // STATUS
        // ===============================
        public StatusLocacao Status { get; set; }
            = StatusLocacao.Aberta;

        public string? Observacao { get; set; }

        public DateTime CriadoEm { get; set; }
            = DateTime.UtcNow;
    }
}