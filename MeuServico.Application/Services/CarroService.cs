using AutoMapper;
using MeuServico.Core.Entities;
using MeuServico.Application.Dtos;

using MeuServico.Application.Interfaces;

namespace MeuServico.Application.Services
{
    public class CarroService
        : BaseService<Carro, CarroDto>
    {
        public CarroService(
            IBaseRepository<Carro> repo,
            IMapper mapper)
            : base(repo, mapper)
        {
        }
    }
}