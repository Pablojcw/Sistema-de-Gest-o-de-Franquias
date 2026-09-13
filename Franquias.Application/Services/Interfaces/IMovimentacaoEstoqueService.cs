using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.MovimentacaoEstoque;

namespace Franquias.Application.Services.Interfaces;

public interface IMovimentacaoEstoqueService
{
    Task<MovimentacaoEstoqueResponse> CriarAsync(CriarMovimentacaoEstoqueRequest request);

    Task<MovimentacaoEstoqueResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<MovimentacaoEstoqueResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20);
}