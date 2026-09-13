using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface ITaxaFranquiaRepository
{
    Task<TaxaFranquia> AdicionarAsync(TaxaFranquia taxaFranquia);

    Task<TaxaFranquia?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<TaxaFranquia>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusTaxaFranquia? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<TaxaFranquia> AtualizarAsync(TaxaFranquia taxaFranquia);
}