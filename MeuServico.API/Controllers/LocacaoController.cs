
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;


namespace MeuServico.API.Controllers
{
    public class LocacaoController
        : BaseController<LocacaoDto>
    {
        public LocacaoController(
            IBaseService<LocacaoDto> service)
            : base(service)
        {
        }
    }
}