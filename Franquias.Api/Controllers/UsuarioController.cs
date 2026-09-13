using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Usuario;
using Franquias.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<UsuarioResponse>> Criar(CriarUsuarioRequest request)
    {
        var usuario = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<UsuarioResponse>> ObterPorId(Guid id)
    {
        var usuario = await _service.ObterPorIdAsync(id);
        if (usuario is null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<List<UsuarioResponse>>> ObterTodos()
    {
        var usuarios = await _service.ObterTodasAsync();
        return Ok(usuarios);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<UsuarioResponse>> Atualizar(Guid id, AtualizarUsuarioRequest request)
    {
        var usuario = await _service.AtualizarAsync(id, request);
        return Ok(usuario);
    }

    [HttpPut("{id:guid}/senha")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<UsuarioResponse>> AlterarSenha(Guid id, AlterarSenhaRequest request)
    {
        var usuario = await _service.AlterarSenhaAsync(id, request);
        return Ok(usuario);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<UsuarioResponse>> Inativar(Guid id)
    {
        var usuario = await _service.AlterarAtivoAsync(id, new AlterarAtivoRequest { Ativo = false });
        return Ok(usuario);
    }
}