using MeuServico.Core.Entities;
using MeuServico.Core.Enums;    

namespace MeuServico.Application.Dtos
{
    public class DespesaGeralDto
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public TipoDespesa TipoDespesa { get; set; }

        // Relacionamento opcional com carro
        public int? CarroId { get; set; }

        public string ? NomeCarro { get; set; } // ✅ CORRETO
        public string ? Observacao { get; set; }
    }
}