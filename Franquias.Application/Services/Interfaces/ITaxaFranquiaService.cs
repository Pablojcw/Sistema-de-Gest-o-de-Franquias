using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.TaxaFranquia;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services.Interfaces;

public interface ITaxaFranquiaService
{
    Task<TaxaFranquiaResponse> CriarAsync(CriarTaxaFranquiaRequest request);

    Task<TaxaFranquiaResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<TaxaFranquiaResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusTaxaFranquia? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<TaxaFranquiaResponse> RegistrarPagamentoAsync(Guid id);
}