using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.MovimentacaoEstoque;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacoesEstoqueController : ControllerBase
{
    private readonly IMovimentacaoEstoqueService _service;

    public MovimentacoesEstoqueController(IMovimentacaoEstoqueService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Criar(CriarMovimentacaoEstoqueRequest request)
    {
        var movimentacao = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = movimentacao.Id }, movimentacao);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> ObterPorId(Guid id)
    {
        var movimentacao = await _service.ObterPorIdAsync(id);
        if (movimentacao is null)
            return NotFound();

        return Ok(movimentacao);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<MovimentacaoEstoqueResponse>>> ObterTodas(
        [FromQuery] Guid? unidadeId = null,
        [FromQuery] Guid? produtoId = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var movimentacoes = await _service.ObterFiltradasAsync(
            unidadeId,
            produtoId,
            dataInicio,
            dataFim,
            pagina,
            tamanhoPagina);

        return Ok(movimentacoes);
    }
}