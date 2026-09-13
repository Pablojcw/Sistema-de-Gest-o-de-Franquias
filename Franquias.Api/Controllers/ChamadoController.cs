using Franquias.Application.DTOs.Chamado;
using Franquias.Application.DTOs.Comum;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Franquias.Domain.Entities;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChamadosController : ControllerBase
{
    private readonly IChamadoService _service;

    public ChamadosController(IChamadoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ChamadoResponse>> Criar(CriarChamadoRequest request)
    {
        var chamado = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = chamado.Id }, chamado);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ChamadoResponse>> ObterPorId(Guid id)
    {
        var chamado = await _service.ObterPorIdAsync(id);
        if (chamado is null)
            return NotFound();

        return Ok(chamado);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<ChamadoResponse>>> ObterTodos(
        [FromQuery] Guid? unidadeId = null,
        [FromQuery] StatusPrioridade? prioridade = null,
        [FromQuery] StatusChamado? status = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var chamados = await _service.ObterFiltradasAsync(
            unidadeId,
            prioridade,
            status,
            pagina,
            tamanhoPagina);

        return Ok(chamados);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ChamadoResponse>> Atualizar(Guid id, AtualizarChamadoRequest request)
    {
        var chamado = await _service.AtualizarAsync(id, request);
        return Ok(chamado);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<ActionResult<ChamadoResponse>> AtualizarStatus(Guid id, AtualizarStatusChamadoRequest request)
    {
        var chamado = await _service.AtualizarStatusAsync(id, request);
        return Ok(chamado);
    }

    [HttpPut("{id:guid}/encerrar")]
    public async Task<ActionResult<ChamadoResponse>> Encerrar(Guid id)
    {
        var chamado = await _service.EncerrarAsync(id);
        return Ok(chamado);
    }
}