using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace MeuServico.API.Controllers
{
    public class DespesaGeralController : Controller
    {
        private readonly IBaseService<DespesaGeralDto> _service;
        private readonly IDespesaGeralService _serviceDespesaGeral;
        private readonly IBaseService<CarroDto> _carroService; // 🔥 NOVO

        public DespesaGeralController(
            IBaseService<DespesaGeralDto> service,
            IDespesaGeralService serviceDespesaGeral,
            IBaseService<CarroDto> carroService // 🔥 NOVO
        )
        {
            _service = service;
            _serviceDespesaGeral = serviceDespesaGeral;
            _carroService = carroService;
        }

        public async Task<IActionResult> Index(
            string filter,
            string placa,
            int? carroId,
            DateTime? inicio,
            DateTime? fim,
            int pagina = 1)
        {
            IEnumerable<DespesaGeralDto> despesas;

            if (carroId.HasValue)
            {
                despesas = await _serviceDespesaGeral.GetByCarroId(carroId.Value);
                ViewBag.Total = await _serviceDespesaGeral.GetTotalPorCarro(carroId.Value);
            }
            else if (inicio.HasValue && fim.HasValue)
            {
                despesas = await _serviceDespesaGeral.GetByPeriodo(inicio.Value, fim.Value);
                ViewBag.Total = despesas.Sum(d => d.Valor);
            }
            else
            {
                despesas = await _serviceDespesaGeral.GetAllWithCarro();
                ViewBag.Total = despesas.Sum(d => d.Valor); // 🔥 CORREÇÃO
            }

            if (!string.IsNullOrEmpty(filter))
            {
                despesas = despesas.Where(d =>
                    d.Descricao != null &&
                    d.Descricao.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(placa))
            {
                despesas = despesas.Where(d =>
                    d.NomeCarro != null &&
                    d.NomeCarro.Contains(placa, StringComparison.OrdinalIgnoreCase));
            }

            var paginado = despesas.ToPagedList(pagina, 10);

            return View(paginado);
        }

        // ============================
        // CREATE
        // ============================

        public async Task<IActionResult> Create()
        {
            await CarregarCarros();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DespesaGeralDto dto)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new {
                        Campo = x.Key,
                        Erros = x.Value.Errors.Select(e => e.ErrorMessage)
                    });

                // breakpoint aqui 🔥

                await CarregarCarros();
                return View(dto);
            }

            // 🔥 FORÇA DATA ATUAL
            dto.Data = DateTime.Now;

            await _service.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        // ============================
        // EDIT
        // ============================

        public async Task<IActionResult> Edit(int id)
        {
            var despesa = await _service.GetById(id);

            if (despesa == null)
                return NotFound();

            await CarregarCarros(); // 🔥 IMPORTANTE

            return View(despesa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DespesaGeralDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCarros();
                return View(dto);
            }

            await _service.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        // ============================
        // DELETE
        // ============================

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // ============================
        // DETAILS
        // ============================

        public async Task<IActionResult> Details(int id)
        {
            var despesa = await _service.GetById(id);

            if (despesa == null)
                return NotFound();

            return View(despesa);
        }

        // ============================
        // 🔥 MÉTODO AUXILIAR
        // ============================

        private async Task CarregarCarros()
        {
            var carros = await _carroService.GetAll();

            ViewBag.Carros = carros.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Placa.ToString()
            }).ToList();
        }
    }
}