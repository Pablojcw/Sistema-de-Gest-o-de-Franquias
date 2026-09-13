using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Produto;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProdutoResponse> CriarAsync(CriarProdutoRequest request)
    {
        var produto = new Produto(
            request.Nome,
            request.Descricao,
            request.Categoria,
            request.PrecoBase,
            request.FranquiaId
        );

        var produtoCriado = await _repository.AdicionarAsync(produto);

        return ParaResponse(produtoCriado);
    }

    public async Task<ProdutoResponse?> ObterPorIdAsync(Guid id)
    {
        var produto = await _repository.ObterPorIdAsync(id);

        return produto is null ? null : ParaResponse(produto);
    }

    public async Task<ResultadoPaginado<ProdutoResponse>> ObterFiltradasAsync(
        string? nome = null,
        string? categoria = null,
        StatusProduto? status = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        var resultado = await _repository.ObterFiltradasAsync(
            nome,
            categoria,
            status,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<ProdutoResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<ProdutoResponse> AtualizarAsync(Guid id, AtualizarProdutoRequest request)
    {
        var produto = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        produto.Atualizar(
            request.Nome,
            request.Descricao,
            request.Categoria,
            request.PrecoBase);

        await _repository.AtualizarAsync(produto);

        return ParaResponse(produto);
    }

    public async Task<ProdutoResponse> AlterarStatusAsync(Guid id, AlterarAtivoRequest request)
    {
        var produto = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        produto.AlterarStatus(request.Ativo ? StatusProduto.Ativo : StatusProduto.Inativo);

        await _repository.AtualizarAsync(produto);

        return ParaResponse(produto);
    }

    private static ProdutoResponse ParaResponse(Produto produto)
    {
        return new ProdutoResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Categoria = produto.Categoria,
            PrecoBase = produto.PrecoBase,
            Status = produto.Status,
            FranquiaId = produto.FranquiaId
        };
    }
}