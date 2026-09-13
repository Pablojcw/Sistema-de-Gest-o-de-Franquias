using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Produto;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutosController(IProdutoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> Criar(CriarProdutoRequest request)
    {
        var produto = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProdutoResponse>> ObterPorId(Guid id)
    {
        var produto = await _service.ObterPorIdAsync(id);
        if (produto is null)
            return NotFound();

        return Ok(produto);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<ProdutoResponse>>> ObterTodos(
        [FromQuery] string? nome = null,
        [FromQuery] string? categoria = null,
        [FromQuery] StatusProduto? status = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var produtos = await _service.ObterFiltradasAsync(nome, categoria, status, pagina, tamanhoPagina);
        return Ok(produtos);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProdutoResponse>> Atualizar(Guid id, AtualizarProdutoRequest request)
    {
        var produto = await _service.AtualizarAsync(id, request);
        return Ok(produto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProdutoResponse>> Inativar(Guid id)
    {
        var produto = await _service.AlterarStatusAsync(id, new AlterarAtivoRequest { Ativo = false });
        return Ok(produto);
    }
}