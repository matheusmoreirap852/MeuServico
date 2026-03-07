using AutoMapper;
using BackEndApi.Dtos;
using BackEndApi.Models;
using BackEndApi.Repositories.Base;

namespace BackEndApi.Service
{
    public class LocacaoService
        : BaseService<Locacao, LocacaoDto>
    {
        public LocacaoService(
            IBaseRepository<Locacao> repo,
            IMapper mapper)
            : base(repo, mapper)
        {
        }
    }
}