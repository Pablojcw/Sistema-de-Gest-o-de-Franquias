using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Usuario;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsuarioResponse> CriarAsync(CriarUsuarioRequest request)
    {
        var existente = await _repository.ObterPorEmailAsync(request.Email);
        if (existente is not null)
            throw new ArgumentException("Já existe um usuário cadastrado com esse e-mail.");

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        var usuario = new Usuario(
            request.Nome,
            request.Email,
            senhaHash,
            request.Perfil,
            request.FranquiaId,
            request.UnidadeId
        );

        var usuarioCriado = await _repository.AdicionarAsync(usuario);

        return ParaResponse(usuarioCriado);
    }

    public async Task<UsuarioResponse?> ObterPorIdAsync(Guid id)
    {
        var usuario = await _repository.ObterPorIdAsync(id);

        return usuario is null ? null : ParaResponse(usuario);
    }

    public async Task<List<UsuarioResponse>> ObterTodasAsync()
    {
        var usuarios = await _repository.ObterTodasAsync();

        return usuarios.Select(ParaResponse).ToList();
    }

    public async Task<UsuarioResponse> AtualizarAsync(Guid id, AtualizarUsuarioRequest request)
    {
        var usuario = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Usuário não encontrado.");

        if (usuario.Email != request.Email)
        {
            var outro = await _repository.ObterPorEmailAsync(request.Email);
            if (outro is not null && outro.Id != id)
                throw new ArgumentException("Já existe um usuário cadastrado com esse e-mail.");
        }

        usuario.Atualizar(
            request.Nome,
            request.Email,
            request.Perfil,
            request.UnidadeId);

        await _repository.AtualizarAsync(usuario);

        return ParaResponse(usuario);
    }

    public async Task<UsuarioResponse> AlterarSenhaAsync(Guid id, AlterarSenhaRequest request)
    {
        var usuario = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Usuário não encontrado.");

        usuario.AlterarSenha(BCrypt.Net.BCrypt.HashPassword(request.Senha));

        await _repository.AtualizarAsync(usuario);

        return ParaResponse(usuario);
    }

    public async Task<UsuarioResponse> AlterarAtivoAsync(Guid id, AlterarAtivoRequest request)
    {
        var usuario = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Usuário não encontrado.");

        usuario.AlterarAtivo(request.Ativo);

        await _repository.AtualizarAsync(usuario);

        return ParaResponse(usuario);
    }

    private static UsuarioResponse ParaResponse(Usuario usuario)
    {
        return new UsuarioResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil,
            Ativa = usuario.Ativa,
            FranquiaId = usuario.FranquiaId,
            UnidadeId = usuario.UnidadeId
        };
    }
}