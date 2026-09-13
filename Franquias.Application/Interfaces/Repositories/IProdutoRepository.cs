using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IProdutoRepository
{
    Task<Produto> AdicionarAsync(Produto produto);

    Task<Produto?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<Produto>> ObterFiltradasAsync(
        string? nome = null,
        string? categoria = null,
        StatusProduto? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<Produto> AtualizarAsync(Produto produto);
}