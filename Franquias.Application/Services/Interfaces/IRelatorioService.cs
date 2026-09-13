using Franquias.Application.DTOs.Relatorios;

namespace Franquias.Application.Services.Interfaces;

public interface IRelatorioService
{
    Task<List<RelatorioFaturamentoResponse>> ObterFaturamentoAsync(
        Guid? unidadeId,
        DateTime dataInicio,
        DateTime dataFim);

    Task<List<RelatorioRankingResponse>> ObterRankingAsync(
        DateTime dataInicio,
        DateTime dataFim,
        int top = 5);

    Task<RelatorioRoyaltyResponse> CalcularRoyaltyAsync(
        Guid unidadeId,
        DateTime dataInicio,
        DateTime dataFim);

    Task<decimal> ObterRoyaltiesGeradosAsync(
        Guid? unidadeId,
        DateTime? dataInicio,
        DateTime? dataFim);

    Task<List<RelatorioProdutoMaisVendidoResponse>> ObterProdutosMaisVendidosAsync(
        DateTime dataInicio,
        DateTime dataFim,
        int top = 5);

    Task<List<RelatorioEstoqueCriticoResponse>> ObterEstoqueCriticoAsync(Guid? unidadeId);

    Task<List<RelatorioChamadoStatusResponse>> ObterChamadosPorStatusAsync(Guid? unidadeId);
}