using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class TaxaFranquiaRepository : ITaxaFranquiaRepository
{
    private readonly AppDbContext _context;

    public TaxaFranquiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaxaFranquia> AdicionarAsync(TaxaFranquia taxaFranquia)
    {
        await _context.TaxasFranquia.AddAsync(taxaFranquia);
        await _context.SaveChangesAsync();

        return taxaFranquia;
    }

    public async Task<TaxaFranquia?> ObterPorIdAsync(Guid id)
    {
        return await _context.TaxasFranquia
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<ResultadoPaginado<TaxaFranquia>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusTaxaFranquia? status = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.TaxasFranquia.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            query = query.Where(t => t.UnidadeId == unidadeId.Value);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        var total = await query.CountAsync();

        var itens = await query
            .OrderByDescending(t => t.DataVencimento)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<TaxaFranquia>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }

    public async Task<TaxaFranquia> AtualizarAsync(TaxaFranquia taxaFranquia)
    {
        _context.Update(taxaFranquia);
        await _context.SaveChangesAsync();

        return taxaFranquia;
    }
}