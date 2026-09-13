using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Royalty;

namespace Franquias.Application.Services.Interfaces;

public interface IRoyaltyService
{
    Task<RoyaltyResponse> CriarAsync(CriarRoyaltyRequest request);

    Task<RoyaltyResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<RoyaltyResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        bool? pago = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<RoyaltyResponse> RegistrarPagamentoAsync(Guid id);
}