using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.DTOs.Fornecedor;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class FornecedorProdutoRepository : IFornecedorProdutoRepository
{
    private readonly AppDbContext _context;

    public FornecedorProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FornecedorProduto> AdicionarAsync(FornecedorProduto fornecedorProduto)
    {
        await _context.FornecedorProdutos.AddAsync(fornecedorProduto);
        await _context.SaveChangesAsync();

        return fornecedorProduto;
    }

    public async Task<FornecedorProduto?> ObterPorIdAsync(Guid fornecedorId, Guid produtoId)
    {
        return await _context.FornecedorProdutos
            .AsNoTracking()
            .FirstOrDefaultAsync(fp => fp.FornecedorId == fornecedorId && fp.ProdutoId == produtoId);
    }

    public async Task<List<FornecedorProdutoResponse>> ObterTodosComNomesAsync()
    {
        var query =
            from fp in _context.FornecedorProdutos.AsNoTracking()
            join f in _context.Fornecedores.AsNoTracking() on fp.FornecedorId equals f.Id
            join p in _context.Produtos.AsNoTracking() on fp.ProdutoId equals p.Id
            select new FornecedorProdutoResponse
            {
                FornecedorId = f.Id,
                FornecedorNome = f.Nome,
                ProdutoId = p.Id,
                ProdutoNome = p.Nome
            };

        return await query
            .OrderBy(x => x.FornecedorNome)
            .ThenBy(x => x.ProdutoNome)
            .ToListAsync();
    }

    public async Task<List<FornecedorProdutoResponse>> ObterPorFornecedorComNomesAsync(Guid fornecedorId)
    {
        var query =
            from fp in _context.FornecedorProdutos.AsNoTracking()
            join f in _context.Fornecedores.AsNoTracking() on fp.FornecedorId equals f.Id
            join p in _context.Produtos.AsNoTracking() on fp.ProdutoId equals p.Id
            where fp.FornecedorId == fornecedorId
            select new FornecedorProdutoResponse
            {
                FornecedorId = f.Id,
                FornecedorNome = f.Nome,
                ProdutoId = p.Id,
                ProdutoNome = p.Nome
            };

        return await query
            .OrderBy(x => x.ProdutoNome)
            .ToListAsync();
    }

    public async Task RemoverAsync(FornecedorProduto fornecedorProduto)
    {
        _context.FornecedorProdutos.Remove(fornecedorProduto);
        await _context.SaveChangesAsync();
    }
}