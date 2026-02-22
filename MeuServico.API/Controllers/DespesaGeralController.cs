
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;

namespace MeuServico.API.Controllers
{
    public class DespesaGeralController
        : BaseController<DespesaGeralDto>
    {
        public DespesaGeralController(
            IBaseService<DespesaGeralDto> service)
            : base(service)
        {
        }
    }
}