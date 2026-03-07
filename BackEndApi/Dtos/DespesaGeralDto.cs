using BackEndApi.Models;

namespace BackEndApi.Dtos
{
    public class DespesaGeralDto
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataDespesa { get; set; }

        public TipoDespesa TipoDespesa { get; set; }

        // Relacionamento opcional com carro
        public int? CarroId { get; set; }
        public Carro? Carro { get; set; }

        public string Observacao { get; set; }
    }
}