using Franquias.Application.DTOs.Fornecedor;

namespace Franquias.Application.Services.Interfaces;

public interface IFornecedorProdutoService
{
    Task<FornecedorProdutoResponse> AssociarAsync(AssociarFornecedorProdutoRequest request);

    Task<List<FornecedorProdutoResponse>> ObterTodasAsync();

    Task<List<FornecedorProdutoResponse>> ObterPorFornecedorAsync(Guid fornecedorId);

    Task RemoverAsync(Guid fornecedorId, Guid produtoId);
}