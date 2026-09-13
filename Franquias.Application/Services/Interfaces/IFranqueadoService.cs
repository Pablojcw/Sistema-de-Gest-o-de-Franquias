using Franquias.Application.DTOs.Franqueado;

namespace Franquias.Application.Services.Interfaces;

public interface IFranqueadoService
{
    Task<FranqueadoResponse> CriarAsync(CriarFranqueadoRequest request);

    Task<FranqueadoResponse?> ObterPorIdAsync(Guid id);

    Task<List<FranqueadoResponse>> ObterTodasAsync();

    Task<FranqueadoResponse> AtualizarAsync(Guid id, AtualizarFranqueadoRequest request);
}