using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Royalty;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class RoyaltyService : IRoyaltyService
{
    private readonly IRoyaltyRepository _repository;
    private readonly IUnidadeRepository _unidadeRepository;

    public RoyaltyService(
        IRoyaltyRepository repository,
        IUnidadeRepository unidadeRepository)
    {
        _repository = repository;
        _unidadeRepository = unidadeRepository;
    }

    public async Task<RoyaltyResponse> CriarAsync(CriarRoyaltyRequest request)
    {
        var unidade = await _unidadeRepository.ObterPorIdAsync(request.UnidadeId)
            ?? throw new KeyNotFoundException("Unidade não encontrada.");

        var royalty = new Royalty(
            request.Porcentagem,
            request.Valor,
            request.DataInicio,
            request.DataFim,
            request.DataVencimento,
            request.UnidadeId
        );

        var royaltyCriado = await _repository.AdicionarAsync(royalty);

        return ParaResponse(royaltyCriado);
    }

    public async Task<RoyaltyResponse?> ObterPorIdAsync(Guid id)
    {
        var royalty = await _repository.ObterPorIdAsync(id);

        return royalty is null ? null : ParaResponse(royalty);
    }

    public async Task<ResultadoPaginado<RoyaltyResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        bool? pago = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        dataInicio = NormalizarParaUtc(dataInicio);
        dataFim = NormalizarParaUtc(dataFim);

        var resultado = await _repository.ObterFiltradasAsync(
            unidadeId,
            pago,
            dataInicio,
            dataFim,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<RoyaltyResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<RoyaltyResponse> RegistrarPagamentoAsync(Guid id)
    {
        var royalty = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Royalty não encontrado.");

        royalty.RegistrarPagamento();

        await _repository.AtualizarAsync(royalty);

        return ParaResponse(royalty);
    }

    private static DateTime? NormalizarParaUtc(DateTime? data)
    {
        return data.HasValue
            ? DateTime.SpecifyKind(data.Value, DateTimeKind.Utc)
            : null;
    }

    private static RoyaltyResponse ParaResponse(Royalty royalty)
    {
        return new RoyaltyResponse
        {
            Id = royalty.Id,
            Porcentagem = royalty.Porcentagem,
            Valor = royalty.Valor,
            DataInicio = royalty.DataInicio,
            DataFim = royalty.DataFim,
            DataVencimento = royalty.DataVencimento,
            Status = royalty.Status,
            UnidadeId = royalty.UnidadeId
        };
    }
}