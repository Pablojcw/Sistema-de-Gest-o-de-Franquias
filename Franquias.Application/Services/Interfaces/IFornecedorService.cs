using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Fornecedor;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services.Interfaces;

public interface IFornecedorService
{
    Task<FornecedorResponse> CriarAsync(CriarFornecedorRequest request);

    Task<FornecedorResponse?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<FornecedorResponse>> ObterFiltradasAsync(
        string? nome = null,
        string? cnpj = null,
        StatusFornecedor? status = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<FornecedorResponse> AtualizarAsync(Guid id, AtualizarFornecedorRequest request);

    Task<FornecedorResponse> AlterarStatusAsync(Guid id, AlterarAtivoRequest request);
}