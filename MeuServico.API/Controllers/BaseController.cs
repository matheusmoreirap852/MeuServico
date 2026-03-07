using MeuServico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuServico.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BaseController<TDto> : ControllerBase
{
    protected readonly IBaseService<TDto> _service;

    public BaseController(IBaseService<TDto> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create(TDto dto)
        => Ok(await _service.Create(dto));

    [HttpPut]
    public async Task<IActionResult> Update(TDto dto)
        => Ok(await _service.Update(dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _service.Delete(id));
}