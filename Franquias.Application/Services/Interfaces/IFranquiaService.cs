using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Franquia;

namespace Franquias.Application.Services.Interfaces;

public interface IFranquiaService
{
    Task<FranquiaResponse> CriarAsync(CriarFranquiaRequest request);

    Task<FranquiaResponse?> ObterPorIdAsync(Guid id);

    Task<List<FranquiaResponse>> ObterTodasAsync();

    Task<FranquiaResponse> AtualizarAsync(Guid id, AtualizarFranquiaRequest request);

    Task<FranquiaResponse> AlterarAtivaAsync(Guid id, AlterarAtivoRequest request);
}