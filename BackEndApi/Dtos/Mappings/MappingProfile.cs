using AutoMapper;
using BackEndApi.Models;

namespace BackEndApi.Dtos.Mappings
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
