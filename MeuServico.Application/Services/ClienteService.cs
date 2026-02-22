using AutoMapper;
using MeuServico.Core.Entities;
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;

namespace MeuServico.Application.Services
{
    public class ClienteService
        : BaseService<Cliente, ClienteDto>
    {
        public ClienteService(
            IBaseRepository<Cliente> repo,
            IMapper mapper)
            : base(repo, mapper)
        {
        }
    }
}