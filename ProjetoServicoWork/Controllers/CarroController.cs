using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace ProjetoServicoWork.ControllersView
{
    public class CarroController : Controller
    {
        private readonly ICarroService _service;
        private readonly IBaseService<EmpresaDto> _empresaService;

        public CarroController(
        ICarroService service,
        IBaseService<EmpresaDto> empresaService)
        {
                _service = service;
                _empresaService = empresaService;
        }

        public async Task<IActionResult> Index(string filter, int pagina = 1)
        {
            var carros = await _service.GetAll();

            if (!string.IsNullOrEmpty(filter))
            {
                carros = carros
                    .Where(c => c.Placa.Contains(filter) ||
                                c.Modelo.Contains(filter))
                    .ToList();
            }

            var paginado = carros.ToPagedList(pagina, 10);

            return View(paginado);
        }

        // ===============================
        // CREATE
        // ===============================

        public async Task<IActionResult> Create()
        {
            await CarregarEmpresas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarroDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarEmpresas();
                return View(dto);
            }

            if (await _service.PlacaJaExiste(dto.Placa))
            {
                ModelState.AddModelError("Placa", "Já existe um carro cadastrado com essa placa.");

                await CarregarEmpresas();
                return View(dto);
            }

            await _service.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // DETAILS
        // ===============================

        public async Task<IActionResult> Details(int id)
        {
            var carro = await _service.GetById(id);

            if (carro == null)
                return NotFound();

            return View(carro);
        }

        // ===============================
        // EDIT
        // ===============================

        public async Task<IActionResult> Edit(int id)
        {
            await CarregarEmpresas();

            var carro = await _service.GetById(id);

            if (carro == null)
                return NotFound();

            return View(carro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarroDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarEmpresas();
                return View(dto);
            }

            await _service.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // DELETE
        // ===============================

        public async Task<IActionResult> Delete(int id)
        {
            var carro = await _service.GetById(id);

            if (carro == null)
                return NotFound();

            return View(carro);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // MÉTODO AUXILIAR
        // ===============================

        private async Task CarregarEmpresas()
        {
            var empresas = await _empresaService.GetAll();

            ViewBag.Empresas = new SelectList(
                empresas,
                "Id",
                "NomeFantasia"
            );
        }
    }
}