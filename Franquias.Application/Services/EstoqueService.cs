using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Estoque;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class EstoqueService : IEstoqueService
{
    private readonly IEstoqueRepository _repository;

    public EstoqueService(IEstoqueRepository repository)
    {
        _repository = repository;
    }

    public async Task<EstoqueResponse> CriarAsync(CriarEstoqueRequest request)
    {
        var existente = await _repository.ObterPorProdutoEUnidadeAsync(
            request.ProdutoId,
            request.UnidadeId);

        if (existente is not null)
            throw new ArgumentException("Já existe estoque cadastrado para esse produto na unidade.");

        var estoque = new Estoque(
            request.Quantidade,
            request.EstoqueMinimo,
            request.ProdutoId,
            request.UnidadeId
        );

        var estoqueCriado = await _repository.AdicionarAsync(estoque);

        return ParaResponse(estoqueCriado);
    }

    public async Task<EstoqueResponse?> ObterPorIdAsync(Guid id)
    {
        var estoque = await _repository.ObterPorIdAsync(id);

        return estoque is null ? null : ParaResponse(estoque);
    }

    public async Task<EstoqueResponse?> ObterPorProdutoEUnidadeAsync(Guid produtoId, Guid unidadeId)
    {
        var estoque = await _repository.ObterPorProdutoEUnidadeAsync(produtoId, unidadeId);

        return estoque is null ? null : ParaResponse(estoque);
    }

    public async Task<ResultadoPaginado<EstoqueResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        bool? abaixoDoMinimo = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        var resultado = await _repository.ObterFiltradasAsync(
            unidadeId,
            produtoId,
            abaixoDoMinimo,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<EstoqueResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<EstoqueResponse> AtualizarEstoqueMinimoAsync(Guid id, AtualizarEstoqueRequest request)
    {
        var estoque = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Estoque não encontrado.");

        estoque.AlterarEstoqueMinimo(request.EstoqueMinimo);

        await _repository.AtualizarAsync(estoque);

        return ParaResponse(estoque);
    }

    private static EstoqueResponse ParaResponse(Estoque estoque)
    {
        return new EstoqueResponse
        {
            Id = estoque.Id,
            Quantidade = estoque.Quantidade,
            EstoqueMinimo = estoque.EstoqueMinimo,
            ProdutoId = estoque.ProdutoId,
            UnidadeId = estoque.UnidadeId
        };
    }
}