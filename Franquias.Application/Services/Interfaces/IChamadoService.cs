using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Chamado;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services.Interfaces;

public interface IChamadoService
{
    Task<ChamadoResponse> CriarAsync(CriarChamadoRequest request);

    Task<ChamadoResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<ChamadoResponse>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusPrioridade? prioridade = null,
        StatusChamado? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<ChamadoResponse> AtualizarAsync(Guid id, AtualizarChamadoRequest request);

    Task<ChamadoResponse> AtualizarStatusAsync(Guid id, AtualizarStatusChamadoRequest request);

    Task<ChamadoResponse> EncerrarAsync(Guid id);
}