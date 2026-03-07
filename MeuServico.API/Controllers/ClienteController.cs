using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;

namespace MeuServico.API.Controllers;

public class ClienteController
    : BaseController<ClienteDto>
{
    public ClienteController(
        IBaseService<ClienteDto> service)
        : base(service)
    {
    }
}