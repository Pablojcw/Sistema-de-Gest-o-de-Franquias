using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class FranqueadoRepository : IFranqueadoRepository
{
    private readonly AppDbContext _context;

    public FranqueadoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Franqueado> AdicionarAsync(Franqueado franqueado)
    {
        await _context.Franqueados.AddAsync(franqueado);
        await _context.SaveChangesAsync();

        return franqueado;
    }

    public async Task<Franqueado?> ObterPorIdAsync(Guid id)
    {
        return await _context.Franqueados
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<List<Franqueado>> ObterTodasAsync()
    {
        return await _context.Franqueados
            .AsNoTracking()
            .OrderBy(f => f.Nome)
            .ToListAsync();
    }

    public async Task<Franqueado> AtualizarAsync(Franqueado franqueado)
    {
        _context.Update(franqueado);
        await _context.SaveChangesAsync();

        return franqueado;
    }
}