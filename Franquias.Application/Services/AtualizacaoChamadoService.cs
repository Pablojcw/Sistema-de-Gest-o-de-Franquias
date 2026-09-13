using Franquias.Application.DTOs.AtualizacaoChamado;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class AtualizacaoChamadoService : IAtualizacaoChamadoService
{
    private readonly IAtualizacaoChamadoRepository _repository;
    private readonly IChamadoRepository _chamadoRepository;

    public AtualizacaoChamadoService(
        IAtualizacaoChamadoRepository repository,
        IChamadoRepository chamadoRepository)
    {
        _repository = repository;
        _chamadoRepository = chamadoRepository;
    }

    public async Task<AtualizacaoChamadoResponse> CriarAsync(CriarAtualizacaoChamadoRequest request)
    {
        var chamado = await _chamadoRepository.ObterPorIdAsync(request.ChamadoId)
            ?? throw new KeyNotFoundException("Chamado não encontrado.");

        var atualizacaoChamado = new AtualizacaoChamado(
            request.Descricao,
            request.UsuarioId,
            request.ChamadoId
        );

        var atualizacaoChamadoCriada = await _repository.AdicionarAsync(atualizacaoChamado);

        return ParaResponse(atualizacaoChamadoCriada);
    }

    public async Task<AtualizacaoChamadoResponse?> ObterPorIdAsync(Guid id)
    {
        var atualizacaoChamado = await _repository.ObterPorIdAsync(id);

        return atualizacaoChamado is null ? null : ParaResponse(atualizacaoChamado);
    }

    public async Task<List<AtualizacaoChamadoResponse>> ObterPorChamadoAsync(Guid chamadoId)
    {
        var atualizacoesChamado = await _repository.ObterPorChamadoAsync(chamadoId);

        return atualizacoesChamado.Select(ParaResponse).ToList();
    }

    private static AtualizacaoChamadoResponse ParaResponse(AtualizacaoChamado atualizacaoChamado)
    {
        return new AtualizacaoChamadoResponse
        {
            Id = atualizacaoChamado.Id,
            Descricao = atualizacaoChamado.Descricao,
            Data = atualizacaoChamado.Data,
            UsuarioId = atualizacaoChamado.UsuarioId,
            ChamadoId = atualizacaoChamado.ChamadoId
        };
    }
}