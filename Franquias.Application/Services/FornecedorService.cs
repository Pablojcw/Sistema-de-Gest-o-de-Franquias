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

    public async Task<FornecedorResponse> CriarAsync (CriarFornecedorRequest request)
    {
        var fornecedor = new Fornecedor(
            request.Nome,
            request.Cnpj,
            request.Email,
            request.Telefone

        );
        var fornecedorCriado = await _repository.AdicionarAsync(fornecedor);
        return new FornecedorResponse
        {
            Id = fornecedorCriado.Id,
            Nome = fornecedorCriado.Nome,
            Cnpj = fornecedorCriado.Cnpj,
            Email = fornecedorCriado.Email,
            Telefone = fornecedorCriado.Telefone,
            Status = fornecedorCriado.Status
        };
    }

    public async Task<FornecedorResponse?> ObterPorIdAsync(Guid id)
    {
        var fornecedor = await _repository.ObterPorIdAsync(id);

        if (fornecedor is null)
            return null;

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

    public async Task<List<FornecedorResponse>> ObterTodasAsync()
    {
        var fornecedor = await _repository.ObterTodasAsync();

        return fornecedor.Select(f => new FornecedorResponse
        {
            Id = f.Id,
            Nome = f.Nome,
            Cnpj = f.Cnpj,
            Email = f.Email,
            Telefone = f.Telefone,
            Status = f.Status
        }).ToList();
    }

}