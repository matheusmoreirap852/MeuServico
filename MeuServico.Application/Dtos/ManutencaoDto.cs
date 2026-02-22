using MeuServico.Core.Entities;
using MeuServico.Core.Enums;

namespace MeuServico.Application.Dtos
{
    public class ManutencaoDto
    {
        public int Id { get; set; }

        // Relacionamento com Carro
        public int CarroId { get; set; }
        public Carro Carro { get; set; }

        // Informações da manutenção
        public TipoManutencao Tipo { get; set; }

        public string DescricaoServico { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataManutencao { get; set; }

        // KM do veículo no dia
        public int KmVeiculo { get; set; }

        // Próxima revisão
        public DateTime? ProximaRevisaoData { get; set; }
        public int? ProximaRevisaoKm { get; set; }

        public string Observacao { get; set; }
    }
}
