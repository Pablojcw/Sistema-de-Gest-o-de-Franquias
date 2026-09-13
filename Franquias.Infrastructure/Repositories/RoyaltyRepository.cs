using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class RoyaltyRepository : IRoyaltyRepository
{
    private readonly AppDbContext _context;

    public RoyaltyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Royalty> AdicionarAsync(Royalty royalty)
    {
        await _context.Royalties.AddAsync(royalty);
        await _context.SaveChangesAsync();

        return royalty;
    }

    public async Task<Royalty?> ObterPorIdAsync(Guid id)
    {
        return await _context.Royalties
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<ResultadoPaginado<Royalty>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        bool? pago = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.Royalties.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            query = query.Where(r => r.UnidadeId == unidadeId.Value);

        if (pago.HasValue)
            query = query.Where(r => pago.Value
                ? r.Status == StatusRoyalty.Pago
                : r.Status != StatusRoyalty.Pago);

        if (dataInicio.HasValue)
            query = query.Where(r => r.DataFim >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(r => r.DataInicio <= dataFim.Value);

        var total = await query.CountAsync();

        var itens = await query
            .OrderByDescending(r => r.DataVencimento)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<Royalty>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }

    public async Task<Royalty> AtualizarAsync(Royalty royalty)
    {
        _context.Update(royalty);
        await _context.SaveChangesAsync();

        return royalty;
    }
}