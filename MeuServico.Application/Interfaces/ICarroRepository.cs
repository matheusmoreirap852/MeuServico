using MeuServico.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeuServico.Application.Interfaces
{
    public interface ICarroRepository : IBaseRepository<Carro>
    {
        Task<bool> ExistePlaca(string placa);
    }
}
