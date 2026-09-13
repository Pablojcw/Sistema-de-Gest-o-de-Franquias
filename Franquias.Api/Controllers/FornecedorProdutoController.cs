using Franquias.Application.DTOs.Fornecedor;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedorProdutosController : ControllerBase
{
    private readonly IFornecedorProdutoService _service;

    public FornecedorProdutosController(IFornecedorProdutoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorProdutoResponse>> Associar(AssociarFornecedorProdutoRequest request)
    {
        var associação = await _service.AssociarAsync(request);
        return CreatedAtAction(
            nameof(ObterTodas),
            new { associação.FornecedorId, associação.ProdutoId },
            associação);
    }

    [HttpGet]
    public async Task<ActionResult<List<FornecedorProdutoResponse>>> ObterTodas()
    {
        var associações = await _service.ObterTodasAsync();
        return Ok(associações);
    }

    [HttpGet("fornecedor/{fornecedorId:guid}")]
    public async Task<ActionResult<List<FornecedorProdutoResponse>>> ObterPorFornecedor(Guid fornecedorId)
    {
        var associações = await _service.ObterPorFornecedorAsync(fornecedorId);
        return Ok(associações);
    }

    [HttpDelete("{fornecedorId:guid}/produto/{produtoId:guid}")]
    public async Task<IActionResult> Remover(Guid fornecedorId, Guid produtoId)
    {
        await _service.RemoverAsync(fornecedorId, produtoId);
        return NoContent();
    }
}