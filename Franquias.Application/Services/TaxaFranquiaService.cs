using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.TaxaFranquia;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class TaxaFranquiaService : ITaxaFranquiaService
{
    private readonly ITaxaFranquiaRepository _repository;
    private readonly IUnidadeRepository _unidadeRepository;

    public TaxaFranquiaService(
        ITaxaFranquiaRepository repository,
        IUnidadeRepository unidadeRepository)
    {
        _repository = repository;
        _unidadeRepository = unidadeRepository;
    }

    public async Task<TaxaFranquiaResponse> CriarAsync(CriarTaxaFranquiaRequest request)
    {
        var unidade = await _unidadeRepository.ObterPorIdAsync(request.UnidadeId)
            ?? throw new KeyNotFoundException("Unidade não encontrada.");

        var taxaFranquia = new TaxaFranquia(
            request.Valor,
            request.Porcentagem,
            request.DataVencimento,
            request.UnidadeId
        );

        var taxaFranquiaCriada = await _repository.AdicionarAsync(taxaFranquia);

        return ParaResponse(taxaFranquiaCriada);
    }

    public async Task<TaxaFranquiaResponse?> ObterPorIdAsync(Guid id)
    {
        var taxaFranquia = await _repository.ObterPorIdAsync(id);

        return taxaFranquia is null ? null : ParaResponse(taxaFranquia);
    }

    public async Task<ResultadoPaginado<TaxaFranquiaResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusTaxaFranquia? status = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        var resultado = await _repository.ObterFiltradasAsync(
            unidadeId,
            status,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<TaxaFranquiaResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<TaxaFranquiaResponse> RegistrarPagamentoAsync(Guid id)
    {
        var taxaFranquia = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Taxa de franquia não encontrada.");

        taxaFranquia.RegistrarPagamento();

        await _repository.AtualizarAsync(taxaFranquia);

        return ParaResponse(taxaFranquia);
    }

    private static TaxaFranquiaResponse ParaResponse(TaxaFranquia taxaFranquia)
    {
        return new TaxaFranquiaResponse
        {
            Id = taxaFranquia.Id,
            Valor = taxaFranquia.Valor,
            Porcentagem = taxaFranquia.Porcentagem,
            DataVencimento = taxaFranquia.DataVencimento,
            Status = taxaFranquia.Status,
            UnidadeId = taxaFranquia.UnidadeId
        };
    }
}