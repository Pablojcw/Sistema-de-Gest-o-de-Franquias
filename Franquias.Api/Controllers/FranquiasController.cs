using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Franquia;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FranquiasController : ControllerBase
{
    private readonly IFranquiaService _service;

    public FranquiasController(IFranquiaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<FranquiaResponse>> Criar(CriarFranquiaRequest request)
    {
        var franquia = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = franquia.Id }, franquia);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FranquiaResponse>> ObterPorId(Guid id)
    {
        var franquia = await _service.ObterPorIdAsync(id);
        if (franquia is null)
            return NotFound();

        return Ok(franquia);
    }

    [HttpGet]
    public async Task<ActionResult<List<FranquiaResponse>>> ObterTodas()
    {
        var franquias = await _service.ObterTodasAsync();
        return Ok(franquias);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FranquiaResponse>> Atualizar(Guid id, AtualizarFranquiaRequest request)
    {
        var franquia = await _service.AtualizarAsync(id, request);
        return Ok(franquia);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<FranquiaResponse>> Inativar(Guid id)
    {
        var franquia = await _service.AlterarAtivaAsync(id, new AlterarAtivoRequest { Ativo = false });
        return Ok(franquia);
    }
}