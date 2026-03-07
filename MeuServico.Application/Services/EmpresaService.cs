using AutoMapper;
using MeuServico.Application.Interfaces;
using MeuServico.Application.Dtos;
using MeuServico.Core.Entities;

namespace MeuServico.Application.Services
{
    public class EmpresaService : BaseService<Empresa, EmpresaDto>
    {
        public EmpresaService(
            IBaseRepository<Empresa> repo,
            IMapper mapper)
            : base(repo, mapper)
        {
        }
    }

}
