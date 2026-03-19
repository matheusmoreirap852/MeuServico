using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using MeuServico.Core.Entities;
using MeuServico.Infrastructure.Persistence;
using MeuServico.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class DespesaGeralRepository
    : BaseRepository<DespesaGeral>, IDespesaGeralRepository
{
    private readonly AppDbContext _context;

    public DespesaGeralRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DespesaGeral>> GetAllWithCarro()
    {
        return await _context.DespesasGerais
            .Include(d => d.Carro) // 🔥 traz dados do carro
            .OrderByDescending(d => d.Data)
            .ToListAsync();
    }

    // 🔎 Buscar por carro
    public async Task<IEnumerable<DespesaGeral>> GetByCarroId(int carroId)
    {
        return await _context.DespesasGerais
            .Include(d => d.Carro) // 🔥 importante
            .Where(d => d.CarroId == carroId)
            .OrderByDescending(d => d.Data)
            .ToListAsync();
    }

    // 📅 Buscar por período
    public async Task<IEnumerable<DespesaGeral>> GetByPeriodo(DateTime inicio, DateTime fim)
    {
        return await _context.DespesasGerais
            .Include(d => d.Carro)
            .Where(d => d.Data >= inicio && d.Data <= fim)
            .OrderByDescending(d => d.Data)
            .ToListAsync();
    }

    // 💰 Total por carro
    public async Task<decimal> GetTotalPorCarro(int carroId)
    {
        return await _context.DespesasGerais
            .Where(d => d.CarroId == carroId)
            .SumAsync(d => d.Valor);
    }
}