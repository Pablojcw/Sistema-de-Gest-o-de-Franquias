using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.ItemVenda;
using Franquias.Application.DTOs.Venda;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{
    private readonly IVendaService _service;

    public VendasController(IVendaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<VendaResponse>> Criar(CriarVendaRequest request)
    {
        var venda = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VendaResponse>> ObterPorId(Guid id)
    {
        var venda = await _service.ObterPorIdAsync(id);
        if (venda is null)
            return NotFound();

        return Ok(venda);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<VendaResponse>>> ObterTodas(
        [FromQuery] Guid? unidadeId = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var vendas = await _service.ObterFiltradasAsync(
            unidadeId,
            dataInicio,
            dataFim,
            pagina,
            tamanhoPagina);

        return Ok(vendas);
    }

    [HttpPost("{id:guid}/itens")]
    public async Task<ActionResult<VendaResponse>> AdicionarItem(Guid id, CriarItemVendaRequest request)
    {
        var venda = await _service.AdicionarItemAsync(id, request);
        return Ok(venda);
    }

    [HttpPut("{id:guid}/confirmar")]
    public async Task<ActionResult<VendaResponse>> Confirmar(Guid id)
    {
        var venda = await _service.ConfirmarAsync(id);
        return Ok(venda);
    }

    [HttpPut("{id:guid}/cancelar")]
    public async Task<ActionResult<VendaResponse>> Cancelar(Guid id)
    {
        var venda = await _service.CancelarAsync(id);
        return Ok(venda);
    }
}