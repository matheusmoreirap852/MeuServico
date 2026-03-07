using AutoMapper;
using MeuServico.Application.Interfaces;
using MeuServico.Application.Dtos;
using MeuServico.Core.Entities;

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