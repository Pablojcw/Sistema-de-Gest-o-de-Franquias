using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IFranqueadoRepository
{
    Task<Franqueado> AdicionarAsync(Franqueado franqueado);

    Task<Franqueado?> ObterPorIdAsync(Guid id);

    Task<List<Franqueado>> ObterTodasAsync();

    Task<Franqueado> AtualizarAsync(Franqueado franqueado);
}