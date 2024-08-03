using AutoMapper;
using BackEndApi.Models;

namespace BackEndApi.Dtos.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<RegistroServico, RegistroServiceDto>().ReverseMap();
        }
    }
}
