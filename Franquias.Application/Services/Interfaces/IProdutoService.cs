using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Produto;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services.Interfaces;

public interface IProdutoService
{
    Task<ProdutoResponse> CriarAsync(CriarProdutoRequest request);

    Task<ProdutoResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<ProdutoResponse>> ObterFiltradasAsync(
        string? nome = null,
        string? categoria = null,
        StatusProduto? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<ProdutoResponse> AtualizarAsync(Guid id, AtualizarProdutoRequest request);

    Task<ProdutoResponse> AlterarStatusAsync(Guid id, AlterarAtivoRequest request);
}