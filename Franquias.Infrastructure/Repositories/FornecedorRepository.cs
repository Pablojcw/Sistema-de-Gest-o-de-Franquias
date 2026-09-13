namespace Franquias.Infrastructure.Repositories;

using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
        
    }

    public async Task<Fornecedor> AdicionarAsync(Fornecedor fornecedor)
    {
        await _context.Fornecedores.AddAsync(fornecedor);
        await _context.SaveChangesAsync();

        return fornecedor;
    }

    public async Task<Fornecedor?> ObterPorIdAsync(Guid id)
    {
        return await _context.Fornecedores
        .AsNoTracking()
        .FirstOrDefaultAsync(f => f.Id == id);
    }
    public async Task<List<Fornecedor>> ObterTodasAsync()
    {
        return await _context.Fornecedores
        .AsNoTracking()
        .ToListAsync();
    }
    
}