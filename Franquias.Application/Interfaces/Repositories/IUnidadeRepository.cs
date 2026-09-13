using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IUnidadeRepository
{
    Task<Unidade> AdicionarAsync(Unidade unidade);

    Task<Unidade?> ObterPorIdAsync(Guid id);

    Task<Unidade?> ObterPorCnpjAsync(string cnpj);

    Task<ResultadoPaginado<Unidade>> ObterFiltradasAsync(
        string? nome = null,
        string? cidade = null,
        string? cnpj = null,
        bool? ativa = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<Unidade> AtualizarAsync(Unidade unidade);
}