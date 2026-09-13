using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IFornecedorRepository
{
    Task<Fornecedor> AdicionarAsync(Fornecedor fornecedor);

    Task<Fornecedor?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<Fornecedor>> ObterFiltradasAsync(
        string? nome = null,
        string? cnpj = null,
        StatusFornecedor? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<Fornecedor> AtualizarAsync(Fornecedor fornecedor);
}