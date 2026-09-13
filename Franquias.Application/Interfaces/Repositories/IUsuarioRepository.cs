using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario> AdicionarAsync(Usuario usuario);

    Task<Usuario?> ObterPorIdAsync(Guid id);

    Task<Usuario?> ObterPorEmailAsync(string email);

    Task<List<Usuario>> ObterTodasAsync();

    Task<Usuario> AtualizarAsync(Usuario usuario);
}