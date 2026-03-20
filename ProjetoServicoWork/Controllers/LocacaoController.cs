using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace MeuServico.API.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly IBaseService<LocacaoDto> _locacaoService;
        private readonly IBaseService<CarroDto> _carroService;
        private readonly IBaseService<ClienteDto> _clienteService;

        public LocacaoController(
            IBaseService<LocacaoDto> locacaoService,
            IBaseService<CarroDto> carroService,
            IBaseService<ClienteDto> clienteService
        )
        {
            _locacaoService = locacaoService;
            _carroService = carroService;
            _clienteService = clienteService;
        }

        // ===============================
        // LISTAGEM
        // ===============================
        public async Task<IActionResult> Index(string filter, int pagina = 1)
        {
            var locacoes = await _locacaoService.GetAll();

            if (!string.IsNullOrEmpty(filter))
            {
                locacoes = locacoes
                    .Where(l => l.Observacao != null && l.Observacao.Contains(filter))
                    .ToList();
            }

            var paginado = locacoes.ToPagedList(pagina, 10);

            return View(paginado);
        }

        // ===============================
        // CREATE
        // ===============================
        public async Task<IActionResult> Create()
        {
            await CarregarCombos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocacaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCombos();
                return View(dto);
            }

            // 💰 calcular valor previsto automaticamente
            dto.QuantidadeDiarias = (dto.DataPrevistaDevolucao - dto.DataInicio).Days;
            dto.ValorTotalPrevisto = dto.QuantidadeDiarias * dto.ValorDiaria;

            await _locacaoService.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // EDIT
        // ===============================
        public async Task<IActionResult> Edit(int id)
        {
            var locacao = await _locacaoService.GetById(id);

            await CarregarCombos();

            return View(locacao);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LocacaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCombos();
                return View(dto);
            }

            // recalcular
            dto.QuantidadeDiarias = (dto.DataPrevistaDevolucao - dto.DataInicio).Days;
            dto.ValorTotalPrevisto = dto.QuantidadeDiarias * dto.ValorDiaria;

            await _locacaoService.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // DELETE
        // ===============================
        public async Task<IActionResult> Delete(int id)
        {
            var locacao = await _locacaoService.GetById(id);
            return View(locacao);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _locacaoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // 🔎 API: Buscar dados do carro
        // ===============================
        [HttpGet]
        public async Task<IActionResult> GetCarro(int carroId)
        {
            var carro = await _carroService.GetById(carroId);

            if (carro == null)
                return NotFound();

            return Json(new
            {
                carro.Id,
                carro.Modelo,
                carro.Placa,
                carro.ValorPago
            });
        }

        // ===============================
        // 🔧 AUXILIAR
        // ===============================
        private async Task CarregarCombos()
        {
            var carros = await _carroService.GetAll();
            var clientes = await _clienteService.GetAll();

            ViewBag.Carros = new SelectList(carros, "Id", "Modelo");
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nome");
        }
    }
}