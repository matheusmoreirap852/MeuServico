using AutoMapper;
using MeuServico.Application.Dtos;
using MeuServico.Core.Entities;


namespace MeuServico.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Carro, CarroDto>().ReverseMap();
            CreateMap<Manutencao, ManutencaoDto>().ReverseMap();
            CreateMap<DespesaGeral, DespesaGeralDto>().ReverseMap();
            CreateMap<Locacao, LocacaoDto>().ReverseMap();
        }
    }
}
