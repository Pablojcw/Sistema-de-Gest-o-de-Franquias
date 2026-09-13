using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.MovimentacaoEstoque;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class MovimentacaoEstoqueService : IMovimentacaoEstoqueService
{
    private readonly IMovimentacaoEstoqueRepository _repository;
    private readonly IEstoqueRepository _estoqueRepository;

    public MovimentacaoEstoqueService(
        IMovimentacaoEstoqueRepository repository,
        IEstoqueRepository estoqueRepository)
    {
        _repository = repository;
        _estoqueRepository = estoqueRepository;
    }

    public async Task<MovimentacaoEstoqueResponse> CriarAsync(CriarMovimentacaoEstoqueRequest request)
    {
        var movimentacaoEstoque = new MovimentacaoEstoque(
            request.Tipo,
            request.Quantidade,
            request.ProdutoId,
            request.UnidadeId,
            request.UsuarioId
        );

        var estoque = await _estoqueRepository.ObterPorProdutoEUnidadeAsync(
            request.ProdutoId,
            request.UnidadeId)
            ?? throw new ArgumentException("Não existe estoque cadastrado para esse produto na unidade.");

        if (request.Tipo == TipoMovimentacaoEstoque.Entrada)
            estoque.Creditar(request.Quantidade);
        else
            estoque.Debitar(request.Quantidade);

        await _estoqueRepository.AtualizarAsync(estoque);

        var movimentacaoCriada = await _repository.AdicionarAsync(movimentacaoEstoque);

        return ParaResponse(movimentacaoCriada);
    }

    public async Task<MovimentacaoEstoqueResponse?> ObterPorIdAsync(Guid id)
    {
        var movimentacaoEstoque = await _repository.ObterPorIdAsync(id);

        return movimentacaoEstoque is null ? null : ParaResponse(movimentacaoEstoque);
    }

    public async Task<ResultadoPaginado<MovimentacaoEstoqueResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        var resultado = await _repository.ObterFiltradasAsync(
            unidadeId,
            produtoId,
            dataInicio,
            dataFim,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<MovimentacaoEstoqueResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    private static DateTime? NormalizarParaUtc(DateTime? data)
    {
        return data.HasValue
            ? DateTime.SpecifyKind(data.Value, DateTimeKind.Utc)
            : null;
    }

    private static MovimentacaoEstoqueResponse ParaResponse(MovimentacaoEstoque movimentacaoEstoque)
    {
        return new MovimentacaoEstoqueResponse
        {
            Id = movimentacaoEstoque.Id,
            Tipo = movimentacaoEstoque.Tipo,
            Quantidade = movimentacaoEstoque.Quantidade,
            Data = movimentacaoEstoque.Data,
            ProdutoId = movimentacaoEstoque.ProdutoId,
            UnidadeId = movimentacaoEstoque.UnidadeId,
            UsuarioId = movimentacaoEstoque.UsuarioId
        };
    }
}