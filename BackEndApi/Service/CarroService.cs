using AutoMapper;
using BackEndApi.Models;
using BackEndApi.Dtos;
using BackEndApi.Repositories.Base;

namespace BackEndApi.Service
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