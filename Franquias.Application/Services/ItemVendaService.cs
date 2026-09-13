using Franquias.Application.DTOs.ItemVenda;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class ItemVendaService : IItemVendaService
{
    private readonly IItemVendaRepository _repository;
    private readonly IVendaRepository _vendaRepository;

    public ItemVendaService(
        IItemVendaRepository repository,
        IVendaRepository vendaRepository)
    {
        _repository = repository;
        _vendaRepository = vendaRepository;
    }

    public async Task<ItemVendaResponse> CriarAsync(CriarItemVendaRequest request)
    {
        var venda = await _vendaRepository.ObterPorIdAsync(request.VendaId)
            ?? throw new KeyNotFoundException("Venda não encontrada.");

        if (venda.Status != StatusVenda.Pendente)
            throw new InvalidOperationException("Somente vendas pendentes podem receber itens.");

        var itemVenda = new ItemVenda(
            request.Quantidade,
            request.PrecoUnitario,
            request.VendaId,
            request.ProdutoId
        );

        var itemVendaCriado = await _repository.AdicionarAsync(itemVenda);

        return ParaResponse(itemVendaCriado);
    }

    public async Task<ItemVendaResponse?> ObterPorIdAsync(Guid id)
    {
        var itemVenda = await _repository.ObterPorIdAsync(id);

        return itemVenda is null ? null : ParaResponse(itemVenda);
    }

    public async Task<List<ItemVendaResponse>> ObterPorVendaAsync(Guid vendaId)
    {
        var itensVenda = await _repository.ObterPorVendaAsync(vendaId);

        return itensVenda.Select(ParaResponse).ToList();
    }

    private static ItemVendaResponse ParaResponse(ItemVenda itemVenda)
    {
        return new ItemVendaResponse
        {
            Id = itemVenda.Id,
            Quantidade = itemVenda.Quantidade,
            PrecoUnitario = itemVenda.PrecoUnitario,
            SubTotal = itemVenda.SubTotal,
            VendaId = itemVenda.VendaId,
            ProdutoId = itemVenda.ProdutoId
        };
    }
}