using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndApi.Models
{
    public class HistoricoValorVenal
    {
        public int Id { get; set; }

        public int CarroId { get; set; }
        public Carro Carro { get; set; }

        public DateTime DataReferencia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorVenal { get; set; }
    }

}
