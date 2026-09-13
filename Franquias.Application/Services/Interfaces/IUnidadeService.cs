using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Unidade;

namespace Franquias.Application.Services.Interfaces;

public interface IUnidadeService
{
    Task<UnidadeResponse> CriarAsync(CriarUnidadeRequest request);

    Task<UnidadeResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<UnidadeResponse>> ObterFiltradasAsync(
        string? nome = null,
        string? cidade = null,
        string? cnpj = null,
        bool? ativa = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<UnidadeResponse> AtualizarAsync(Guid id, AtualizarUnidadeRequest request);

    Task<UnidadeResponse> AlterarSituacaoAsync(Guid id, AlterarAtivoRequest request);
}