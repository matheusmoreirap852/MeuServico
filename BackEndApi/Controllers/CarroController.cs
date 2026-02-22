using BackEndApi.Dtos;

public class CarroController
    : BaseController<CarroDto>
{
    public CarroController(
        IBaseService<CarroDto> service)
        : base(service)
    {
    }
}