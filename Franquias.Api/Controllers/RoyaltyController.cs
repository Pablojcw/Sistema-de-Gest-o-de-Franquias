using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Royalty;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoyaltiesController : ControllerBase
{
    private readonly IRoyaltyService _service;

    public RoyaltiesController(IRoyaltyService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<RoyaltyResponse>> Criar(CriarRoyaltyRequest request)
    {
        var royalty = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = royalty.Id }, royalty);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoyaltyResponse>> ObterPorId(Guid id)
    {
        var royalty = await _service.ObterPorIdAsync(id);
        if (royalty is null)
            return NotFound();

        return Ok(royalty);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<RoyaltyResponse>>> ObterTodos(
        [FromQuery] Guid? unidadeId = null,
        [FromQuery] bool? pago = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var royalties = await _service.ObterFiltradasAsync(
            unidadeId,
            pago,
            dataInicio,
            dataFim,
            pagina,
            tamanhoPagina);

        return Ok(royalties);
    }

    [HttpPut("{id:guid}/pagar")]
    public async Task<ActionResult<RoyaltyResponse>> RegistrarPagamento(Guid id)
    {
        var royalty = await _service.RegistrarPagamentoAsync(id);
        return Ok(royalty);
    }
}