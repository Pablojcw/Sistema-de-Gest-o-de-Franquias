using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Estoque;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstoquesController : ControllerBase
{
    private readonly IEstoqueService _service;

    public EstoquesController(IEstoqueService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<EstoqueResponse>> Criar(CriarEstoqueRequest request)
    {
        var estoque = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = estoque.Id }, estoque);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EstoqueResponse>> ObterPorId(Guid id)
    {
        var estoque = await _service.ObterPorIdAsync(id);
        if (estoque is null)
            return NotFound();

        return Ok(estoque);
    }

    [HttpGet("produto/{produtoId:guid}/unidade/{unidadeId:guid}")]
    public async Task<ActionResult<EstoqueResponse>> ObterPorProdutoEUnidade(Guid produtoId, Guid unidadeId)
    {
        var estoque = await _service.ObterPorProdutoEUnidadeAsync(produtoId, unidadeId);
        if (estoque is null)
            return NotFound();

        return Ok(estoque);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<EstoqueResponse>>> ObterTodos(
        [FromQuery] Guid? unidadeId = null,
        [FromQuery] Guid? produtoId = null,
        [FromQuery] bool? abaixoDoMinimo = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var estoques = await _service.ObterFiltradasAsync(
            unidadeId,
            produtoId,
            abaixoDoMinimo,
            pagina,
            tamanhoPagina);

        return Ok(estoques);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EstoqueResponse>> AtualizarEstoqueMinimo(Guid id, AtualizarEstoqueRequest request)
    {
        var estoque = await _service.AtualizarEstoqueMinimoAsync(id, request);
        return Ok(estoque);
    }
}