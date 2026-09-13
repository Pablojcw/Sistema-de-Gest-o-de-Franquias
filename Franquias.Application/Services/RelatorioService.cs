using Franquias.Application.DTOs.Relatorios;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;

namespace Franquias.Application.Services;

public class RelatorioService : IRelatorioService
{
    private readonly IRelatorioRepository _repository;
    private readonly IUnidadeRepository _unidadeRepository;

    public RelatorioService(
        IRelatorioRepository repository,
        IUnidadeRepository unidadeRepository)
    {
        _repository = repository;
        _unidadeRepository = unidadeRepository;
    }

    public async Task<List<RelatorioFaturamentoResponse>> ObterFaturamentoAsync(
        Guid? unidadeId,
        DateTime dataInicio,
        DateTime dataFim)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        return await _repository.ObterFaturamentoAsync(unidadeId, dataInicio, dataFim);
    }

    public async Task<List<RelatorioRankingResponse>> ObterRankingAsync(
        DateTime dataInicio,
        DateTime dataFim,
        int top = 5)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        if (top < 1)
            top = 1;

        var faturamentos = await _repository.ObterFaturamentoAsync(null, dataInicio, dataFim);

        return faturamentos
            .Take(top)
            .Select((item, indice) => new RelatorioRankingResponse
            {
                Posicao = indice + 1,
                UnidadeId = item.UnidadeId,
                UnidadeNome = item.UnidadeNome,
                Faturamento = item.Faturamento
            })
            .ToList();
    }

    public async Task<RelatorioRoyaltyResponse> CalcularRoyaltyAsync(
        Guid unidadeId,
        DateTime dataInicio,
        DateTime dataFim)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        var unidade = await _unidadeRepository.ObterPorIdAsync(unidadeId)
            ?? throw new KeyNotFoundException("Unidade não encontrada.");

        var faturamento = await _repository.ObterTotalVendasAsync(unidadeId, dataInicio, dataFim);

        var porcentagem = await _repository.ObterPorcentagemRoyaltyAsync(unidadeId)
            ?? throw new KeyNotFoundException("Nenhuma porcentagem de royalty cadastrada para a unidade.");

        var royaltyCalculado = faturamento * (porcentagem / 100);

        return new RelatorioRoyaltyResponse
        {
            UnidadeId = unidadeId,
            UnidadeNome = unidade.Nome,
            Faturamento = faturamento,
            Porcentagem = porcentagem,
            RoyaltyCalculado = royaltyCalculado
        };
    }

    public async Task<decimal> ObterRoyaltiesGeradosAsync(
        Guid? unidadeId,
        DateTime? dataInicio,
        DateTime? dataFim)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        return await _repository.ObterRoyaltiesGeradosAsync(unidadeId, dataInicio, dataFim);
    }

    public async Task<List<RelatorioProdutoMaisVendidoResponse>> ObterProdutosMaisVendidosAsync(
        DateTime dataInicio,
        DateTime dataFim,
        int top = 5)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        if (top < 1)
            top = 1;

        return await _repository.ObterProdutosMaisVendidosAsync(dataInicio, dataFim, top);
    }

    public async Task<List<RelatorioEstoqueCriticoResponse>> ObterEstoqueCriticoAsync(Guid? unidadeId)
    {
        return await _repository.ObterEstoqueCriticoAsync(unidadeId);
    }

    public async Task<List<RelatorioChamadoStatusResponse>> ObterChamadosPorStatusAsync(Guid? unidadeId)
    {
        return await _repository.ObterChamadosPorStatusAsync(unidadeId);
    }

    private static DateTime NormalizarParaUtc(DateTime data)
    {
        return data.Kind == DateTimeKind.Utc
            ? data
            : DateTime.SpecifyKind(data, DateTimeKind.Utc);
    }

    private static DateTime? NormalizarParaUtc(DateTime? data)
    {
        return data.HasValue ? NormalizarParaUtc(data.Value) : null;
    }
}