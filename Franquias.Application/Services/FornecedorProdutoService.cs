using Franquias.Application.DTOs.Fornecedor;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class FornecedorProdutoService : IFornecedorProdutoService
{
    private readonly IFornecedorProdutoRepository _repository;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IProdutoRepository _produtoRepository;

    public FornecedorProdutoService(
        IFornecedorProdutoRepository repository,
        IFornecedorRepository fornecedorRepository,
        IProdutoRepository produtoRepository)
    {
        _repository = repository;
        _fornecedorRepository = fornecedorRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<FornecedorProdutoResponse> AssociarAsync(AssociarFornecedorProdutoRequest request)
    {
        var fornecedor = await _fornecedorRepository.ObterPorIdAsync(request.FornecedorId)
            ?? throw new KeyNotFoundException("Fornecedor não encontrado.");

        var produto = await _produtoRepository.ObterPorIdAsync(request.ProdutoId)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        var existente = await _repository.ObterPorIdAsync(request.FornecedorId, request.ProdutoId);
        if (existente is not null)
            throw new ArgumentException("Esse produto já está associado ao fornecedor.");

        var associação = new FornecedorProduto(request.FornecedorId, request.ProdutoId);

        await _repository.AdicionarAsync(associação);

        return new FornecedorProdutoResponse
        {
            FornecedorId = fornecedor.Id,
            FornecedorNome = fornecedor.Nome,
            ProdutoId = produto.Id,
            ProdutoNome = produto.Nome
        };
    }

    public async Task<List<FornecedorProdutoResponse>> ObterTodasAsync()
    {
        return await _repository.ObterTodosComNomesAsync();
    }

    public async Task<List<FornecedorProdutoResponse>> ObterPorFornecedorAsync(Guid fornecedorId)
    {
        return await _repository.ObterPorFornecedorComNomesAsync(fornecedorId);
    }

    public async Task RemoverAsync(Guid fornecedorId, Guid produtoId)
    {
        var associação = await _repository.ObterPorIdAsync(fornecedorId, produtoId)
            ?? throw new KeyNotFoundException("Associação entre fornecedor e produto não encontrada.");

        await _repository.RemoverAsync(associação);
    }
}