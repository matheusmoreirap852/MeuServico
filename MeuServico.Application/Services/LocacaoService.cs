using AutoMapper;
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using MeuServico.Core.Entities;

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