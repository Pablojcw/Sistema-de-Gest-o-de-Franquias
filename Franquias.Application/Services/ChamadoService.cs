using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Chamado;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class ChamadoService : IChamadoService
{
    private readonly IChamadoRepository _repository;

    public ChamadoService(IChamadoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ChamadoResponse> CriarAsync(CriarChamadoRequest request)
    {
        var chamado = new Chamado(
            request.Titulo,
            request.Descricao,
            request.Categoria,
            request.UsuarioId,
            request.UnidadeId,
            request.Prioridade
        );

        var chamadoCriado = await _repository.AdicionarAsync(chamado);

        return ParaResponse(chamadoCriado);
    }

    public async Task<ChamadoResponse?> ObterPorIdAsync(Guid id)
    {
        var chamado = await _repository.ObterPorIdAsync(id);

        return chamado is null ? null : ParaResponse(chamado);
    }

    public async Task<ResultadoPaginado<ChamadoResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusPrioridade? prioridade = null,
        StatusChamado? status = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        var resultado = await _repository.ObterFiltradasAsync(
            unidadeId,
            prioridade,
            status,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<ChamadoResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<ChamadoResponse> AtualizarAsync(Guid id, AtualizarChamadoRequest request)
    {
        var chamado = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Chamado não encontrado.");

        chamado.Atualizar(
            request.Titulo,
            request.Descricao,
            request.Categoria,
            request.Prioridade);

        await _repository.AtualizarAsync(chamado);

        return ParaResponse(chamado);
    }

    public async Task<ChamadoResponse> AtualizarStatusAsync(Guid id, AtualizarStatusChamadoRequest request)
    {
        var chamado = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Chamado não encontrado.");

        chamado.AtualizarStatus(request.Status);

        await _repository.AtualizarAsync(chamado);

        return ParaResponse(chamado);
    }

    public async Task<ChamadoResponse> EncerrarAsync(Guid id)
    {
        var chamado = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Chamado não encontrado.");

        chamado.Encerrar();

        await _repository.AtualizarAsync(chamado);

        return ParaResponse(chamado);
    }

    private static ChamadoResponse ParaResponse(Chamado chamado)
    {
        return new ChamadoResponse
        {
            Id = chamado.Id,
            Titulo = chamado.Titulo,
            Descricao = chamado.Descricao,
            Categoria = chamado.Categoria,
            Status = chamado.Status,
            Prioridade = chamado.Prioridade,
            DataAberta = chamado.DataAberta,
            DataFechamento = chamado.DataFechamento,
            UsuarioId = chamado.UsuarioId,
            UnidadeId = chamado.UnidadeId
        };
    }
}