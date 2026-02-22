using AutoMapper;
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using MeuServico.Core.Entities;
namespace BackEndApi.Service
{
    public class ManutencaoService
        : BaseService<Manutencao, ManutencaoDto>
    {
        public ManutencaoService(
            IBaseRepository<Manutencao> repo,
            IMapper mapper)
            : base(repo, mapper)
        {
        }
    }
}