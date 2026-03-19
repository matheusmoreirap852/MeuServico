using MeuServico.Application.Dtos;
using MeuServico.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeuServico.Application.Interfaces
{
    public interface IDespesaGeralRepository : IBaseRepository<DespesaGeral>
    {
        Task<IEnumerable<DespesaGeral>> GetAllWithCarro();
        Task<IEnumerable<DespesaGeral>> GetByCarroId(int carroId);
        Task<IEnumerable<DespesaGeral>> GetByPeriodo(DateTime inicio, DateTime fim);
        Task<decimal> GetTotalPorCarro(int carroId);
    }
}
