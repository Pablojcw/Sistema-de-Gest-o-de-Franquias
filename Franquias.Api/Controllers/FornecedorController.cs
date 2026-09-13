

using Franquias.Application.DTOs.Fornecedor;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly IFornecedorService _service;

    public FornecedoresController(IFornecedorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorResponse>> Criar (
        CriarFornecedorRequest request
    )
    {
        var fornecedor = await _service.CriarAsync(request);
        return CreatedAtAction(
            nameof(ObterPorId),
            new {id = fornecedor.Id},
            fornecedor
        );
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FornecedorResponse>> ObterPorId(Guid id)
    {
        var forncedor = await _service.ObterPorIdAsync(id);
        if(forncedor is null)
        return NotFound();

        return Ok(forncedor);
    }
    [HttpGet]
    public async Task<ActionResult<List<FornecedorResponse>>> ObterTodas()
    {
        var fornecedor = await _service.ObterTodasAsync();
        return Ok(fornecedor);
    }



}
