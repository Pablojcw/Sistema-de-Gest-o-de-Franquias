using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class ItemVendaRepository : IItemVendaRepository
{
    private readonly AppDbContext _context;

    public ItemVendaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ItemVenda> AdicionarAsync(ItemVenda itemVenda)
    {
        await _context.ItensVenda.AddAsync(itemVenda);
        await _context.SaveChangesAsync();

        return itemVenda;
    }

    public async Task<ItemVenda?> ObterPorIdAsync(Guid id)
    {
        return await _context.ItensVenda
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<ItemVenda>> ObterPorVendaAsync(Guid vendaId)
    {
        return await _context.ItensVenda
            .AsNoTracking()
            .Where(i => i.VendaId == vendaId)
            .ToListAsync();
    }
}