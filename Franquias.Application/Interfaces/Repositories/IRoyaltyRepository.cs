using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IRoyaltyRepository
{
    Task<Royalty> AdicionarAsync(Royalty royalty);

    Task<Royalty?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<Royalty>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        bool? pago = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<Royalty> AtualizarAsync(Royalty royalty);
}