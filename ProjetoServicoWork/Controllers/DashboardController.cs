using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoServicoWork.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IBaseService<CarroDto> _carroService;

        public DashboardController(IBaseService<CarroDto> carroService)
        {
            _carroService = carroService;
        }

        public async Task<IActionResult> Index()
        {
            var carros = await _carroService.GetAll();

            ViewBag.TotalCarros = carros.Count();

            ViewBag.CarrosDisponiveis =
                carros.Count(c => c.Status == MeuServico.Core.Enums.StatusCarro.Disponivel);

            ViewBag.CarrosAlugados =
                carros.Count(c => c.Status == MeuServico.Core.Enums.StatusCarro.Alugado);

            return View();
        }
    }
}
