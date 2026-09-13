using Franquias.Application.DTOs.AtualizacaoChamado;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtualizacoesChamadoController : ControllerBase
{
    private readonly IAtualizacaoChamadoService _service;

    public AtualizacoesChamadoController(IAtualizacaoChamadoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<AtualizacaoChamadoResponse>> Criar(CriarAtualizacaoChamadoRequest request)
    {
        var atualizacao = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = atualizacao.Id }, atualizacao);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AtualizacaoChamadoResponse>> ObterPorId(Guid id)
    {
        var atualizacao = await _service.ObterPorIdAsync(id);
        if (atualizacao is null)
            return NotFound();

        return Ok(atualizacao);
    }

    [HttpGet("chamado/{chamadoId:guid}")]
    public async Task<ActionResult<List<AtualizacaoChamadoResponse>>> ObterPorChamado(Guid chamadoId)
    {
        var atualizacoes = await _service.ObterPorChamadoAsync(chamadoId);
        return Ok(atualizacoes);
    }
}