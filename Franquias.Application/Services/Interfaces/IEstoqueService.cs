using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Estoque;

namespace Franquias.Application.Services.Interfaces;

public interface IEstoqueService
{
    Task<EstoqueResponse> CriarAsync(CriarEstoqueRequest request);

    Task<EstoqueResponse?> ObterPorIdAsync(Guid id);

    Task<EstoqueResponse?> ObterPorProdutoEUnidadeAsync(Guid produtoId, Guid unidadeId);

    Task<ResultadoPaginado<EstoqueResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        bool? abaixoDoMinimo = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<EstoqueResponse> AtualizarEstoqueMinimoAsync(Guid id, AtualizarEstoqueRequest request);
}