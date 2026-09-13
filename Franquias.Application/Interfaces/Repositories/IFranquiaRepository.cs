using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IFranquiaRepository
{
    Task<Franquia> AdicionarAsync(Franquia franquia);

    Task<Franquia?> ObterPorIdAsync(Guid id);

    Task<List<Franquia>> ObterTodasAsync();

    Task<Franquia> AtualizarAsync(Franquia franquia);
}