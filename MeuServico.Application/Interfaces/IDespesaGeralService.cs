using MeuServico.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeuServico.Application.Interfaces
{
    public interface IDespesaGeralService : IBaseService<DespesaGeralDto>
    {
        Task<IEnumerable<DespesaGeralDto>> GetAllWithCarro();
        Task<IEnumerable<DespesaGeralDto>> GetByCarroId(int carroId);
        Task<IEnumerable<DespesaGeralDto>> GetByPeriodo(DateTime inicio, DateTime fim);
        Task<decimal> GetTotalPorCarro(int carroId);
    }
}
