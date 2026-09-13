using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IEstoqueRepository
{
    Task<Estoque> AdicionarAsync(Estoque estoque);

    Task<Estoque?> ObterPorIdAsync(Guid id);

    Task<Estoque?> ObterPorProdutoEUnidadeAsync(Guid produtoId, Guid unidadeId);

    Task<ResultadoPaginado<Estoque>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        bool? abaixoDoMinimo = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<Estoque> AtualizarAsync(Estoque estoque);
}