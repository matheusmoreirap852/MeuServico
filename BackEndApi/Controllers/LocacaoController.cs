using BackEndApi.Dtos;
using BackEndApi.Service.IService;

namespace BackEndApi.Controllers
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