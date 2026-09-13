using Franquias.Application.DTOs.Franquia;

namespace Franquias.Application.Services.Interfaces;

public interface IFranquiaService
{
    Task<FranquiaResponse> CriarAsync(CriarFranquiaRequest request);

    Task<FranquiaResponse?> ObterPorIdAsync(Guid id);

    Task<List<FranquiaResponse>> ObterTodasAsync();
}