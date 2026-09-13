using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class FranquiaRepository : IFranquiaRepository
{
    private readonly AppDbContext _context;

    public FranquiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Franquia> AdicionarAsync(Franquia franquia)
    {
        await _context.Franquias.AddAsync(franquia);
        await _context.SaveChangesAsync();

        return franquia;
    }

    public async Task<Franquia?> ObterPorIdAsync(Guid id)
    {
        return await _context.Franquias
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<List<Franquia>> ObterTodasAsync()
    {
        return await _context.Franquias
            .AsNoTracking()
            .ToListAsync();
    }
}