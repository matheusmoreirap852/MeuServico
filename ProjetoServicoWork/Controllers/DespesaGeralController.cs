using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MeuServico.API.Controllers
{
    public class DespesaGeralController : Controller
    {
        private readonly IBaseService<DespesaGeralDto> _service;
        private readonly IDespesaGeralService _serviceDespesaGeral;

        public DespesaGeralController(IBaseService<DespesaGeralDto> service, IDespesaGeralService serviceDespesaGeral)
        {
            _service = service;
            _serviceDespesaGeral = serviceDespesaGeral;
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

            // 🔥 prioridade: filtros mais específicos
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
            }

            // 🔎 filtro por descrição
            if (!string.IsNullOrEmpty(filter))
            {
                despesas = despesas
                    .Where(d => d.Descricao != null &&
                                d.Descricao.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            // 🚗 filtro por placa
            if (!string.IsNullOrEmpty(placa))
            {
                despesas = despesas
                    .Where(d => d.NomeCarro != null &&
                                d.NomeCarro.Contains(placa, StringComparison.OrdinalIgnoreCase));
            }

            var paginado = despesas.ToPagedList(pagina, 10);

            return View(paginado);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST - CREATE
        [HttpPost]
        public async Task<IActionResult> Create(DespesaGeralDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        // GET - EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var despesa = await _service.GetById(id);

            if (despesa == null)
                return NotFound();

            return View(despesa);
        }

        // POST - EDIT
        [HttpPost]
        public async Task<IActionResult> Edit(DespesaGeralDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var despesa = await _service.GetById(id);

            if (despesa == null)
                return NotFound();

            return View(despesa);
        }
    }
}