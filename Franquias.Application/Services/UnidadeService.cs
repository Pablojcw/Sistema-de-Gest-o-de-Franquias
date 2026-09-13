using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Unidade;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class UnidadeService : IUnidadeService
{
    private readonly IUnidadeRepository _repository;

    public UnidadeService(IUnidadeRepository repository)
    {
        _repository = repository;
    }

    public async Task<UnidadeResponse> CriarAsync(CriarUnidadeRequest request)
    {
        var cnpj = string.Concat(request.Cnpj.Where(char.IsDigit));

        var existente = await _repository.ObterPorCnpjAsync(cnpj);
        if (existente is not null)
            throw new ArgumentException("Já existe uma unidade cadastrada com esse CNPJ.");

        var unidade = new Unidade(
            request.Nome,
            cnpj,
            request.Endereco,
            request.Cidade,
            request.Estado,
            request.Email,
            request.Telefone,
            request.DataInicio,
            request.FranqueadoId,
            request.FranquiaId
        );

        var unidadeCriada = await _repository.AdicionarAsync(unidade);

        return ParaResponse(unidadeCriada);
    }

    public async Task<UnidadeResponse?> ObterPorIdAsync(Guid id)
    {
        var unidade = await _repository.ObterPorIdAsync(id);

        return unidade is null ? null : ParaResponse(unidade);
    }

    public async Task<ResultadoPaginado<UnidadeResponse>> ObterFiltradasAsync(
        string? nome = null,
        string? cidade = null,
        string? cnpj = null,
        bool? ativa = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        var resultado = await _repository.ObterFiltradasAsync(
            nome,
            cidade,
            cnpj,
            ativa,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<UnidadeResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<UnidadeResponse> AtualizarAsync(Guid id, AtualizarUnidadeRequest request)
    {
        var unidade = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Unidade não encontrada.");

        unidade.Atualizar(
            request.Nome,
            request.Email,
            request.Telefone,
            request.Endereco,
            request.Cidade,
            request.Estado);

        await _repository.AtualizarAsync(unidade);

        return ParaResponse(unidade);
    }

    public async Task<UnidadeResponse> AlterarSituacaoAsync(Guid id, AlterarAtivoRequest request)
    {
        var unidade = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Unidade não encontrada.");

        unidade.AlterarSituacao(request.Ativo);

        await _repository.AtualizarAsync(unidade);

        return ParaResponse(unidade);
    }

    private static UnidadeResponse ParaResponse(Unidade unidade)
    {
        return new UnidadeResponse
        {
            Id = unidade.Id,
            Nome = unidade.Nome,
            Cnpj = unidade.Cnpj,
            Email = unidade.Email,
            Telefone = unidade.Telefone,
            Endereco = unidade.Endereco,
            Cidade = unidade.Cidade,
            Estado = unidade.Estado,
            DataInicio = unidade.DataInicio,
            Situacao = unidade.Situacao,
            FranquiaId = unidade.FranquiaId,
            FranqueadoId = unidade.FranqueadoId
        };
    }
}