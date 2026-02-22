using BackEndApi.Dtos;
using BackEndApi.Service.IService;

namespace BackEndApi.Controllers
{
    public class ManutencaoController
        : BaseController<ManutencaoDto>
    {
        public ManutencaoController(
            IBaseService<ManutencaoDto> service)
            : base(service)
        {
        }
    }
}