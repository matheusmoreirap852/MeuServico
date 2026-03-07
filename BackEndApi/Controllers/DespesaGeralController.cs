using BackEndApi.Dtos;
using BackEndApi.Service.IService;

namespace BackEndApi.Controllers
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