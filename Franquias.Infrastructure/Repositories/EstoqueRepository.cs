using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class EstoqueRepository : IEstoqueRepository
{
    private readonly AppDbContext _context;

    public EstoqueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Estoque> AdicionarAsync(Estoque estoque)
    {
        await _context.Estoques.AddAsync(estoque);
        await _context.SaveChangesAsync();

        return estoque;
    }

    public async Task<Estoque?> ObterPorIdAsync(Guid id)
    {
        return await _context.Estoques
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Estoque?> ObterPorProdutoEUnidadeAsync(Guid produtoId, Guid unidadeId)
    {
        return await _context.Estoques
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.ProdutoId == produtoId && e.UnidadeId == unidadeId);
    }

    public async Task<ResultadoPaginado<Estoque>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        bool? abaixoDoMinimo = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.Estoques.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            query = query.Where(e => e.UnidadeId == unidadeId.Value);

        if (produtoId.HasValue)
            query = query.Where(e => e.ProdutoId == produtoId.Value);

        if (abaixoDoMinimo.HasValue)
        {
            if (abaixoDoMinimo.Value)
                query = query.Where(e => e.Quantidade <= e.EstoqueMinimo);
            else
                query = query.Where(e => e.Quantidade > e.EstoqueMinimo);
        }

        var total = await query.CountAsync();

        var itens = await query
            .OrderBy(e => e.UnidadeId)
            .ThenBy(e => e.ProdutoId)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<Estoque>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }

    public async Task<Estoque> AtualizarAsync(Estoque estoque)
    {
        _context.Update(estoque);
        await _context.SaveChangesAsync();

        return estoque;
    }
}