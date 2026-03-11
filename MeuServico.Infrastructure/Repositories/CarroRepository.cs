using MeuServico.Application.Interfaces;
using MeuServico.Core.Entities;
using MeuServico.Infrastructure.Persistence;
using MeuServico.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuServico.Infrastructure.Repositories
{
    public class CarroRepository : BaseRepository<Carro>, ICarroRepository
    {
        private readonly AppDbContext _context;

        public CarroRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistePlaca(string placa)
        {
            return await _context.Carros
                .AnyAsync(c => c.Placa == placa);
        }
    }
}