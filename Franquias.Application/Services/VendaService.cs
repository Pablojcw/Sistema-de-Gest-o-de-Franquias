using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.ItemVenda;
using Franquias.Application.DTOs.Venda;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _repository;
    private readonly IItemVendaRepository _itemVendaRepository;
    private readonly IEstoqueRepository _estoqueRepository;
    private readonly IUnidadeRepository _unidadeRepository;

    public VendaService(
        IVendaRepository repository,
        IItemVendaRepository itemVendaRepository,
        IEstoqueRepository estoqueRepository,
        IUnidadeRepository unidadeRepository)
    {
        _repository = repository;
        _itemVendaRepository = itemVendaRepository;
        _estoqueRepository = estoqueRepository;
        _unidadeRepository = unidadeRepository;
    }

    public async Task<VendaResponse> CriarAsync(CriarVendaRequest request)
    {
        var unidade = await _unidadeRepository.ObterPorIdAsync(request.UnidadeId)
            ?? throw new KeyNotFoundException("Unidade não encontrada.");

        if (!unidade.EstaAtiva())
            throw new ArgumentException("Uma unidade inativa não pode registrar vendas.");

        var venda = new Venda(request.UnidadeId);

        var vendaCriada = await _repository.AdicionarAsync(venda);

        return ParaResponse(vendaCriada);
    }

    public async Task<VendaResponse?> ObterPorIdAsync(Guid id)
    {
        var venda = await _repository.ObterPorIdAsync(id);

        return venda is null ? null : ParaResponse(venda);
    }

    public async Task<ResultadoPaginado<VendaResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        var resultado = await _repository.ObterFiltradasAsync(
            unidadeId,
            dataInicio,
            dataFim,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<VendaResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<VendaResponse> AdicionarItemAsync(Guid vendaId, CriarItemVendaRequest request)
    {
        var venda = await ObterPendenteAsync(vendaId);

        var itemVenda = new ItemVenda(
            request.Quantidade,
            request.PrecoUnitario,
            vendaId,
            request.ProdutoId
        );

        await _itemVendaRepository.AdicionarAsync(itemVenda);

        return ParaResponse(venda);
    }

    public async Task<VendaResponse> ConfirmarAsync(Guid id)
    {
        var venda = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Venda não encontrada.");

        if (venda.Status != StatusVenda.Pendente)
            throw new InvalidOperationException("Somente vendas pendentes podem ser confirmadas.");

        var itens = await _itemVendaRepository.ObterPorVendaAsync(id);

        if (itens.Count == 0)
            throw new ArgumentException("A venda precisa possuir pelo menos um item para ser confirmada.");

        var total = itens.Sum(i => i.SubTotal);

        foreach (var item in itens)
        {
            var estoque = await _estoqueRepository.ObterPorProdutoEUnidadeAsync(
                item.ProdutoId,
                venda.UnidadeId)
                ?? throw new ArgumentException("Produto sem estoque cadastrado na unidade.");

            estoque.Debitar(item.Quantidade);

            await _estoqueRepository.AtualizarAsync(estoque);
        }

        venda.Confirmar(total);

        await _repository.AtualizarAsync(venda);

        return ParaResponse(venda);
    }

    public async Task<VendaResponse> CancelarAsync(Guid id)
    {
        var venda = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Venda não encontrada.");

        venda.Cancelar();

        await _repository.AtualizarAsync(venda);

        return ParaResponse(venda);
    }

    private static DateTime? NormalizarParaUtc(DateTime? data)
    {
        return data.HasValue
            ? DateTime.SpecifyKind(data.Value, DateTimeKind.Utc)
            : null;
    }

    private async Task<Venda> ObterPendenteAsync(Guid vendaId)
    {
        var venda = await _repository.ObterPorIdAsync(vendaId)
            ?? throw new KeyNotFoundException("Venda não encontrada.");

        if (venda.Status != StatusVenda.Pendente)
            throw new InvalidOperationException("Somente vendas pendentes podem receber itens.");

        return venda;
    }

    private static VendaResponse ParaResponse(Venda venda)
    {
        return new VendaResponse
        {
            Id = venda.Id,
            Data = venda.Data,
            ValorTotal = venda.ValorTotal,
            Status = venda.Status,
            UnidadeId = venda.UnidadeId
        };
    }
}