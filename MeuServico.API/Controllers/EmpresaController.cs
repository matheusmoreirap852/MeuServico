using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuServico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController
        : BaseController<EmpresaDto>
    {
        public EmpresaController(
            IBaseService<EmpresaDto> service)
            : base(service)
        {
        }
    }
}