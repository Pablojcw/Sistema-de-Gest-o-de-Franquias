using Franquias.Application.DTOs.Fornecedor;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IFornecedorProdutoRepository
{
    Task<FornecedorProduto> AdicionarAsync(FornecedorProduto fornecedorProduto);

    Task<FornecedorProduto?> ObterPorIdAsync(Guid fornecedorId, Guid produtoId);

    Task<List<FornecedorProdutoResponse>> ObterTodosComNomesAsync();

    Task<List<FornecedorProdutoResponse>> ObterPorFornecedorComNomesAsync(Guid fornecedorId);

    Task RemoverAsync(FornecedorProduto fornecedorProduto);
}