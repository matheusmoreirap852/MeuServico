using MeuServico.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeuServico.Application.Interfaces
{
    public interface ICarroService : IBaseService<CarroDto>
    {
        Task<bool> PlacaJaExiste(string placa);
    }
}

