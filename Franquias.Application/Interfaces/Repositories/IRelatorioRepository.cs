using Franquias.Application.DTOs.Relatorios;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IRelatorioRepository
{
    Task<List<RelatorioFaturamentoResponse>> ObterFaturamentoAsync(
        Guid? unidadeId,
        DateTime dataInicio,
        DateTime dataFim);

    Task<decimal> ObterTotalVendasAsync(
        Guid unidadeId,
        DateTime dataInicio,
        DateTime dataFim);

    Task<decimal?> ObterPorcentagemRoyaltyAsync(Guid unidadeId);

    Task<decimal> ObterRoyaltiesGeradosAsync(
        Guid? unidadeId,
        DateTime? dataInicio,
        DateTime? dataFim);

    Task<List<RelatorioProdutoMaisVendidoResponse>> ObterProdutosMaisVendidosAsync(
        DateTime dataInicio,
        DateTime dataFim,
        int top);

    Task<List<RelatorioEstoqueCriticoResponse>> ObterEstoqueCriticoAsync(Guid? unidadeId);

    Task<List<RelatorioChamadoStatusResponse>> ObterChamadosPorStatusAsync(Guid? unidadeId);
}