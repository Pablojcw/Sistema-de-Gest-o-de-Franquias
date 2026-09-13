using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IItemVendaRepository
{
    Task<ItemVenda> AdicionarAsync(ItemVenda itemVenda);

    Task<ItemVenda?> ObterPorIdAsync(Guid id);

    Task<List<ItemVenda>> ObterPorVendaAsync(Guid vendaId);
}