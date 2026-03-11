using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ProjetoServicoWork.ControllersView
{
    public class ClienteController : Controller
    {
        private readonly IBaseService<ClienteDto> _service;

        public ClienteController(IBaseService<ClienteDto> service)
        {
            _service = service;
        }

        // ===============================
        // INDEX
        // ===============================

        public async Task<IActionResult> Index(string filter, int pagina = 1)
        {
            var clientes = await _service.GetAll();

            if (!string.IsNullOrEmpty(filter))
            {
                clientes = clientes
                    .Where(c => c.Nome.Contains(filter) ||
                                c.CPF.Contains(filter))
                    .ToList();
            }

            var paginado = clientes.ToPagedList(pagina, 10);

            return View(paginado);
        }

        // ===============================
        // CREATE
        // ===============================

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.Set(dto);

            return RedirectToAction("Index");
        }

        // ===============================
        // DETAILS
        // ===============================

        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _service.GetById(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // ===============================
        // EDIT
        // ===============================

        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _service.GetById(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // DELETE
        // ===============================

        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _service.GetById(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}