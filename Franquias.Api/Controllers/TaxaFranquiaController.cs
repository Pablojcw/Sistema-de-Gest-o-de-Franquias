using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.TaxaFranquia;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Franquias.Domain.Entities;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxasFranquiaController : ControllerBase
{
    private readonly ITaxaFranquiaService _service;

    public TaxasFranquiaController(ITaxaFranquiaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<TaxaFranquiaResponse>> Criar(CriarTaxaFranquiaRequest request)
    {
        var taxaFranquia = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = taxaFranquia.Id }, taxaFranquia);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaxaFranquiaResponse>> ObterPorId(Guid id)
    {
        var taxaFranquia = await _service.ObterPorIdAsync(id);
        if (taxaFranquia is null)
            return NotFound();

        return Ok(taxaFranquia);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<TaxaFranquiaResponse>>> ObterTodas(
        [FromQuery] Guid? unidadeId = null,
        [FromQuery] StatusTaxaFranquia? status = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var taxasFranquia = await _service.ObterFiltradasAsync(
            unidadeId,
            status,
            pagina,
            tamanhoPagina);

        return Ok(taxasFranquia);
    }

    [HttpPut("{id:guid}/pagar")]
    public async Task<ActionResult<TaxaFranquiaResponse>> RegistrarPagamento(Guid id)
    {
        var taxaFranquia = await _service.RegistrarPagamentoAsync(id);
        return Ok(taxaFranquia);
    }
}