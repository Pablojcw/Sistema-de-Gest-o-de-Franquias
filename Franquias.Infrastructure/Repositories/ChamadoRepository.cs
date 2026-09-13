using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class ChamadoRepository : IChamadoRepository
{
    private readonly AppDbContext _context;

    public ChamadoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Chamado> AdicionarAsync(Chamado chamado)
    {
        await _context.Chamados.AddAsync(chamado);
        await _context.SaveChangesAsync();

        return chamado;
    }

    public async Task<Chamado?> ObterPorIdAsync(Guid id)
    {
        return await _context.Chamados
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ResultadoPaginado<Chamado>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        StatusPrioridade? prioridade = null,
        StatusChamado? status = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.Chamados.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            query = query.Where(c => c.UnidadeId == unidadeId.Value);

        if (prioridade.HasValue)
            query = query.Where(c => c.Prioridade == prioridade.Value);

        if (status.HasValue)
            query = query.Where(c => c.Status == status.Value);

        var total = await query.CountAsync();

        var itens = await query
            .OrderByDescending(c => c.DataAberta)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<Chamado>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }

    public async Task<Chamado> AtualizarAsync(Chamado chamado)
    {
        _context.Update(chamado);
        await _context.SaveChangesAsync();

        return chamado;
    }
}