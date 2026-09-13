using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Fornecedor;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly IFornecedorService _service;
    private readonly IFornecedorProdutoService _fornecedorProdutoService;

    public FornecedoresController(
        IFornecedorService service,
        IFornecedorProdutoService fornecedorProdutoService)
    {
        _service = service;
        _fornecedorProdutoService = fornecedorProdutoService;
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorResponse>> Criar(CriarFornecedorRequest request)
    {
        var fornecedor = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = fornecedor.Id }, fornecedor);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FornecedorResponse>> ObterPorId(Guid id)
    {
        var fornecedor = await _service.ObterPorIdAsync(id);
        if (fornecedor is null)
            return NotFound();

        return Ok(fornecedor);
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPaginado<FornecedorResponse>>> ObterTodos(
        [FromQuery] string? nome = null,
        [FromQuery] string? cnpj = null,
        [FromQuery] StatusFornecedor? status = null,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        var fornecedores = await _service.ObterFiltradasAsync(nome, cnpj, status, pagina, tamanhoPagina);
        return Ok(fornecedores);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FornecedorResponse>> Atualizar(Guid id, AtualizarFornecedorRequest request)
    {
        var fornecedor = await _service.AtualizarAsync(id, request);
        return Ok(fornecedor);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<FornecedorResponse>> Inativar(Guid id)
    {
        var fornecedor = await _service.AlterarStatusAsync(id, new AlterarAtivoRequest { Ativo = false });
        return Ok(fornecedor);
    }

    [HttpGet("{id:guid}/produtos")]
    public async Task<ActionResult<List<FornecedorProdutoResponse>>> ObterProdutos(Guid id)
    {
        var associações = await _fornecedorProdutoService.ObterPorFornecedorAsync(id);
        return Ok(associações);
    }
}