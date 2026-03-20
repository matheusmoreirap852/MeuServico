using MeuServico.Core.Enums;

namespace MeuServico.Core.Entities
{
    public class DespesaGeral
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public TipoDespesa TipoDespesa { get; set; }

        // Relacionamento opcional com carro
        public int? CarroId { get; set; }
        
        
        // 🔥 NAVIGATION PROPERTY (ESSA LINHA É A CHAVE)
        public Carro? Carro { get; set; }
    }
}