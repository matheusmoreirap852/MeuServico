using AutoMapper;
using BackEndApi.Dtos;
using BackEndApi.Models;
using BackEndApi.Repositories.Base;

namespace BackEndApi.Service
{
    public class DespesaGeralService
        : BaseService<DespesaGeral, DespesaGeralDto>
    {
        public DespesaGeralService(
            IBaseRepository<DespesaGeral> repo,
            IMapper mapper)
            : base(repo, mapper)
        {
        }
    }
}