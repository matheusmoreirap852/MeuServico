using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;

namespace MeuServico.API.Controllers;

public class CarroController
    : BaseController<CarroDto>
{
    public CarroController(
        IBaseService<CarroDto> service)
        : base(service)
    {
    }
}