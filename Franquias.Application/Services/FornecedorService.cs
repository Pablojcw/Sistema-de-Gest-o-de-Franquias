using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Fornecedor;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class FornecedorService : IFornecedorService
{
    private readonly IFornecedorRepository _repository;

    public FornecedorService(IFornecedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<FornecedorResponse> CriarAsync(CriarFornecedorRequest request)
    {
        var cnpj = string.Concat(request.Cnpj.Where(char.IsDigit));

        var fornecedor = new Fornecedor(
            request.Nome,
            cnpj,
            request.Email,
            request.Telefone
        );

        var fornecedorCriado = await _repository.AdicionarAsync(fornecedor);

        return ParaResponse(fornecedorCriado);
    }

    public async Task<FornecedorResponse?> ObterPorIdAsync(Guid id)
    {
        var fornecedor = await _repository.ObterPorIdAsync(id);

        return fornecedor is null ? null : ParaResponse(fornecedor);
    }

    public async Task<ResultadoPaginado<FornecedorResponse>> ObterFiltradasAsync(
        string? nome = null,
        string? cnpj = null,
        StatusFornecedor? status = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        var resultado = await _repository.ObterFiltradasAsync(
            nome,
            cnpj,
            status,
            pagina,
            tamanhoPagina);

        return new ResultadoPaginado<FornecedorResponse>
        {
            Pagina = resultado.Pagina,
            TamanhoPagina = resultado.TamanhoPagina,
            TotalItens = resultado.TotalItens,
            TotalPaginas = resultado.TotalPaginas,
            Itens = resultado.Itens.Select(ParaResponse).ToList()
        };
    }

    public async Task<FornecedorResponse> AtualizarAsync(Guid id, AtualizarFornecedorRequest request)
    {
        var fornecedor = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Fornecedor não encontrado.");

        var cnpj = string.Concat(request.Cnpj.Where(char.IsDigit));

        fornecedor.Atualizar(
            request.Nome,
            cnpj,
            request.Email,
            request.Telefone);

        await _repository.AtualizarAsync(fornecedor);

        return ParaResponse(fornecedor);
    }

    public async Task<FornecedorResponse> AlterarStatusAsync(Guid id, AlterarAtivoRequest request)
    {
        var fornecedor = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Fornecedor não encontrado.");

        fornecedor.AlterarStatus(request.Ativo ? StatusFornecedor.Ativo : StatusFornecedor.Inativo);

        await _repository.AtualizarAsync(fornecedor);

        return ParaResponse(fornecedor);
    }

    private static FornecedorResponse ParaResponse(Fornecedor fornecedor)
    {
        return new FornecedorResponse
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Cnpj = fornecedor.Cnpj,
            Email = fornecedor.Email,
            Telefone = fornecedor.Telefone,
            Status = fornecedor.Status
        };
    }
}