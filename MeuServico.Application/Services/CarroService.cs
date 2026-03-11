using AutoMapper;
using MeuServico.Core.Entities;
using MeuServico.Application.Dtos;

using MeuServico.Application.Interfaces;

namespace MeuServico.Application.Services
{
    public class CarroService
    : BaseService<Carro, CarroDto>, ICarroService
    {
        private readonly ICarroRepository _repo;

        public CarroService(
            ICarroRepository repo,
            IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public async Task<bool> PlacaJaExiste(string placa)
        {
            placa = placa.Trim().ToUpper();

            return await _repo.ExistePlaca(placa);
        }
    }
}