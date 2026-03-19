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

            CreateMap<DespesaGeral, DespesaGeralDto>()
             .ForMember(dest => dest.NomeCarro,
                 opt => opt.MapFrom(src => src.Carro.Placa))
             .ReverseMap();

            CreateMap<Locacao, LocacaoDto>().ReverseMap();
            CreateMap<Empresa, EmpresaDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}
