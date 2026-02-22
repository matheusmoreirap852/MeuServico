using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;


namespace MeuServico.API.Controllers;

public class ManutencaoController
    : BaseController<ManutencaoDto>
{
    public ManutencaoController(
        IBaseService<ManutencaoDto> service)
        : base(service)
    {
    }
}