using Franquias.Application.DTOs.ItemVenda;

namespace Franquias.Application.Services.Interfaces;

public interface IItemVendaService
{
    Task<ItemVendaResponse> CriarAsync(CriarItemVendaRequest request);

    Task<ItemVendaResponse?> ObterPorIdAsync(Guid id);

    Task<List<ItemVendaResponse>> ObterPorVendaAsync(Guid vendaId);
}