using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class AtualizacaoChamadoRepository : IAtualizacaoChamadoRepository
{
    private readonly AppDbContext _context;

    public AtualizacaoChamadoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AtualizacaoChamado> AdicionarAsync(AtualizacaoChamado atualizacaoChamado)
    {
        await _context.AtualizacoesChamado.AddAsync(atualizacaoChamado);
        await _context.SaveChangesAsync();

        return atualizacaoChamado;
    }

    public async Task<AtualizacaoChamado?> ObterPorIdAsync(Guid id)
    {
        return await _context.AtualizacoesChamado
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<AtualizacaoChamado>> ObterPorChamadoAsync(Guid chamadoId)
    {
        return await _context.AtualizacoesChamado
            .AsNoTracking()
            .Where(a => a.ChamadoId == chamadoId)
            .OrderByDescending(a => a.Data)
            .ToListAsync();
    }
}