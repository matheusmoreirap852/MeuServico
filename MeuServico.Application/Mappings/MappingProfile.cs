using AutoMapper;
using MeuServico.Application.Dtos;
using MeuServico.Core.Entities;

namespace MeuServico.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Carro, CarroDto>().ReverseMap();
            CreateMap<Manutencao, ManutencaoDto>().ReverseMap();

            // ✅ DespesaGeral corrigido
            CreateMap<DespesaGeral, DespesaGeralDto>()
                .ForMember(dest => dest.NomeCarro,
                    opt => opt.MapFrom(src => src.Carro != null ? src.Carro.Placa : null));

            CreateMap<DespesaGeralDto, DespesaGeral>()
                .ForMember(dest => dest.Carro, opt => opt.Ignore());

            CreateMap<Locacao, LocacaoDto>().ReverseMap();
            CreateMap<Empresa, EmpresaDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}