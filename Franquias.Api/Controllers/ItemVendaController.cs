using Franquias.Application.DTOs.ItemVenda;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItensVendaController : ControllerBase
{
    private readonly IItemVendaService _service;

    public ItensVendaController(IItemVendaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ItemVendaResponse>> Criar(CriarItemVendaRequest request)
    {
        var itemVenda = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = itemVenda.Id }, itemVenda);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemVendaResponse>> ObterPorId(Guid id)
    {
        var itemVenda = await _service.ObterPorIdAsync(id);
        if (itemVenda is null)
            return NotFound();

        return Ok(itemVenda);
    }

    [HttpGet("venda/{vendaId:guid}")]
    public async Task<ActionResult<List<ItemVendaResponse>>> ObterPorVenda(Guid vendaId)
    {
        var itensVenda = await _service.ObterPorVendaAsync(vendaId);
        return Ok(itensVenda);
    }
}