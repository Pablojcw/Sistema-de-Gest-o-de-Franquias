using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class MovimentacaoEstoqueRepository : IMovimentacaoEstoqueRepository
{
    private readonly AppDbContext _context;

    public MovimentacaoEstoqueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MovimentacaoEstoque> AdicionarAsync(MovimentacaoEstoque movimentacaoEstoque)
    {
        await _context.MovimentacoesEstoque.AddAsync(movimentacaoEstoque);
        await _context.SaveChangesAsync();

        return movimentacaoEstoque;
    }

    public async Task<MovimentacaoEstoque?> ObterPorIdAsync(Guid id)
    {
        return await _context.MovimentacoesEstoque
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<ResultadoPaginado<MovimentacaoEstoque>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.MovimentacoesEstoque.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            query = query.Where(m => m.UnidadeId == unidadeId.Value);

        if (produtoId.HasValue)
            query = query.Where(m => m.ProdutoId == produtoId.Value);

        if (dataInicio.HasValue)
            query = query.Where(m => m.Data >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(m => m.Data <= dataFim.Value);

        var total = await query.CountAsync();

        var itens = await query
            .OrderByDescending(m => m.Data)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<MovimentacaoEstoque>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }
}