using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IAtualizacaoChamadoRepository
{
    Task<AtualizacaoChamado> AdicionarAsync(AtualizacaoChamado atualizacaoChamado);

    Task<AtualizacaoChamado?> ObterPorIdAsync(Guid id);

    Task<List<AtualizacaoChamado>> ObterPorChamadoAsync(Guid chamadoId);
}