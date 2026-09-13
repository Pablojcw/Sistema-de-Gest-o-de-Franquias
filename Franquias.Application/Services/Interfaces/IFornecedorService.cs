using Franquias.Application.DTOs.Fornecedor;

namespace Franquias.Application.Services.Interfaces;

public interface IFornecedorService
{
    Task<FornecedorResponse> CriarAsync(CriarFornecedorRequest request);

    Task<FornecedorResponse?> ObterPorIdAsync(Guid id);

    Task<List<FornecedorResponse>> ObterTodasAsync();
}