using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly AppDbContext _context;

    public VendaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Venda> AdicionarAsync(Venda venda)
    {
        await _context.Vendas.AddAsync(venda);
        await _context.SaveChangesAsync();

        return venda;
    }

    public async Task<Venda?> ObterPorIdAsync(Guid id)
    {
        return await _context.Vendas
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<ResultadoPaginado<Venda>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.Vendas.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            query = query.Where(v => v.UnidadeId == unidadeId.Value);

        if (dataInicio.HasValue)
            query = query.Where(v => v.Data >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(v => v.Data <= dataFim.Value);

        var total = await query.CountAsync();

        var itens = await query
            .OrderByDescending(v => v.Data)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<Venda>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }

    public async Task<Venda> AtualizarAsync(Venda venda)
    {
        _context.Update(venda);
        await _context.SaveChangesAsync();

        return venda;
    }
}