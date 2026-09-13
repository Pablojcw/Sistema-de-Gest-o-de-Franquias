using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IChamadoRepository
{
    Task<Chamado> AdicionarAsync(Chamado chamado);

    Task<Chamado?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<Chamado>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusPrioridade? prioridade = null,
        StatusChamado? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<Chamado> AtualizarAsync(Chamado chamado);
}