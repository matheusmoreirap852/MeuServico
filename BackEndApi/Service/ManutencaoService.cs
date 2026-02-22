using AutoMapper;
using BackEndApi.Dtos;
using BackEndApi.Models;
using BackEndApi.Repositories.Base;

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