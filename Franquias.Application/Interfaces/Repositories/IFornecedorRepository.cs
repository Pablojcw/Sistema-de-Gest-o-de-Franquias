using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IFornecedorRepository
{
    Task<Fornecedor> AdicionarAsync(Fornecedor fornecedor);

    Task<Fornecedor?> ObterPorIdAsync(Guid id);

    Task<List<Fornecedor>> ObterTodasAsync();
}