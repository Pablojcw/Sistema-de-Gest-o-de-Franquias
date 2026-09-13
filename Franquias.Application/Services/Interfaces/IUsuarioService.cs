using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Usuario;

namespace Franquias.Application.Services.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioResponse> CriarAsync(CriarUsuarioRequest request);

    Task<UsuarioResponse?> ObterPorIdAsync(Guid id);

    Task<List<UsuarioResponse>> ObterTodasAsync();

    Task<UsuarioResponse> AtualizarAsync(Guid id, AtualizarUsuarioRequest request);

    Task<UsuarioResponse> AlterarSenhaAsync(Guid id, AlterarSenhaRequest request);

    Task<UsuarioResponse> AlterarAtivoAsync(Guid id, AlterarAtivoRequest request);
}