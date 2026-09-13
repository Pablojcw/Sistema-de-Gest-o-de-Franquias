using Franquias.Application.DTOs.AtualizacaoChamado;

namespace Franquias.Application.Services.Interfaces;

public interface IAtualizacaoChamadoService
{
    Task<AtualizacaoChamadoResponse> CriarAsync(CriarAtualizacaoChamadoRequest request);

    Task<AtualizacaoChamadoResponse?> ObterPorIdAsync(Guid id);

    Task<List<AtualizacaoChamadoResponse>> ObterPorChamadoAsync(Guid chamadoId);
}