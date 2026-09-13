using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.ItemVenda;
using Franquias.Application.DTOs.Venda;

namespace Franquias.Application.Services.Interfaces;

public interface IVendaService
{
    Task<VendaResponse> CriarAsync(CriarVendaRequest request);

    Task<VendaResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<VendaResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<VendaResponse> AdicionarItemAsync(Guid vendaId, CriarItemVendaRequest request);

    Task<VendaResponse> ConfirmarAsync(Guid id);

    Task<VendaResponse> CancelarAsync(Guid id);
}