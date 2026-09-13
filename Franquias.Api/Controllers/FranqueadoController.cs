using Franquias.Application.DTOs.Franqueado;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FranqueadosController : ControllerBase
{
    private readonly IFranqueadoService _service;

    public FranqueadosController(IFranqueadoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<FranqueadoResponse>> Criar(CriarFranqueadoRequest request)
    {
        var franqueado = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = franqueado.Id }, franqueado);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FranqueadoResponse>> ObterPorId(Guid id)
    {
        var franqueado = await _service.ObterPorIdAsync(id);
        if (franqueado is null)
            return NotFound();

        return Ok(franqueado);
    }

    [HttpGet]
    public async Task<ActionResult<List<FranqueadoResponse>>> ObterTodos()
    {
        var franqueados = await _service.ObterTodasAsync();
        return Ok(franqueados);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FranqueadoResponse>> Atualizar(Guid id, AtualizarFranqueadoRequest request)
    {
        var franqueado = await _service.AtualizarAsync(id, request);
        return Ok(franqueado);
    }
}