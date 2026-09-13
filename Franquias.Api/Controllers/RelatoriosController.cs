using Franquias.Application.DTOs.Relatorios;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _service;

    public RelatoriosController(IRelatorioService service)
    {
        _service = service;
    }

    [HttpGet("faturamento")]
    public async Task<ActionResult<List<RelatorioFaturamentoResponse>>> ObterFaturamento(
        [FromQuery] Guid? unidadeId,
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim)
    {
        return Ok(await _service.ObterFaturamentoAsync(unidadeId, dataInicio, dataFim));
    }

    [HttpGet("ranking-unidades")]
    public async Task<ActionResult<List<RelatorioRankingResponse>>> ObterRanking(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim,
        [FromQuery] int top = 5)
    {
        return Ok(await _service.ObterRankingAsync(dataInicio, dataFim, top));
    }

    [HttpGet("royalties-calcular")]
    public async Task<ActionResult<RelatorioRoyaltyResponse>> CalcularRoyalty(
        [FromQuery] Guid unidadeId,
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim)
    {
        return Ok(await _service.CalcularRoyaltyAsync(unidadeId, dataInicio, dataFim));
    }

    [HttpGet("royalties-gerados")]
    public async Task<ActionResult<decimal>> ObterRoyaltiesGerados(
        [FromQuery] Guid? unidadeId,
        [FromQuery] DateTime? dataInicio,
        [FromQuery] DateTime? dataFim)
    {
        return Ok(await _service.ObterRoyaltiesGeradosAsync(unidadeId, dataInicio, dataFim));
    }

    [HttpGet("produtos-mais-vendidos")]
    public async Task<ActionResult<List<RelatorioProdutoMaisVendidoResponse>>> ObterProdutosMaisVendidos(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim,
        [FromQuery] int top = 5)
    {
        return Ok(await _service.ObterProdutosMaisVendidosAsync(dataInicio, dataFim, top));
    }

    [HttpGet("estoque-critico")]
    public async Task<ActionResult<List<RelatorioEstoqueCriticoResponse>>> ObterEstoqueCritico(
        [FromQuery] Guid? unidadeId)
    {
        return Ok(await _service.ObterEstoqueCriticoAsync(unidadeId));
    }

    [HttpGet("chamados-por-status")]
    public async Task<ActionResult<List<RelatorioChamadoStatusResponse>>> ObterChamadosPorStatus(
        [FromQuery] Guid? unidadeId)
    {
        return Ok(await _service.ObterChamadosPorStatusAsync(unidadeId));
    }
}