using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Unidade;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadesController : ControllerBase
{
    private readonly IUnidadeService _service;

    public UnidadesController(IUnidadeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<UnidadeResponse>> Criar(CriarUnidadeRequest request)
    {
        var unidade = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = unidade.Id }, unidade);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UnidadeResponse>> ObterPorId(Guid id)
    {
        var unidade = await _service.ObterPorIdAsync(id);
        if (unidade is null)
            return NotFound();

        return Ok(unidade);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<UnidadeResponse>>> ObterTodas(
        [FromQuery] string? nome = null,
        [FromQuery] string? cidade = null,
        [FromQuery] string? cnpj = null,
        [FromQuery] bool? ativa = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var unidades = await _service.ObterFiltradasAsync(nome, cidade, cnpj, ativa, pagina, tamanhoPagina);
        return Ok(unidades);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UnidadeResponse>> Atualizar(Guid id, AtualizarUnidadeRequest request)
    {
        var unidade = await _service.AtualizarAsync(id, request);
        return Ok(unidade);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<UnidadeResponse>> Inativar(Guid id)
    {
        var unidade = await _service.AlterarSituacaoAsync(id, new AlterarAtivoRequest { Ativo = false });
        return Ok(unidade);
    }
}