using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class UnidadeRepository : IUnidadeRepository
{
    private readonly AppDbContext _context;

    public UnidadeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unidade> AdicionarAsync(Unidade unidade)
    {
        await _context.Unidades.AddAsync(unidade);
        await _context.SaveChangesAsync();

        return unidade;
    }

    public async Task<Unidade?> ObterPorIdAsync(Guid id)
    {
        return await _context.Unidades
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Unidade?> ObterPorCnpjAsync(string cnpj)
    {
        return await _context.Unidades
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Cnpj == cnpj);
    }

    public async Task<ResultadoPaginado<Unidade>> ObterFiltradasAsync(
        string? nome = null,
        string? cidade = null,
        string? cnpj = null,
        bool? ativa = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.Unidades.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(u => u.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(u => u.Cidade.Contains(cidade));

        if (!string.IsNullOrWhiteSpace(cnpj))
            query = query.Where(u => u.Cnpj == cnpj);

        if (ativa.HasValue)
            query = query.Where(u => u.Situacao == (ativa.Value ? "Ativa" : "Inativa"));

        var total = await query.CountAsync();

        var itens = await query
            .OrderBy(u => u.Nome)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<Unidade>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }

    public async Task<Unidade> AtualizarAsync(Unidade unidade)
    {
        _context.Update(unidade);
        await _context.SaveChangesAsync();

        return unidade;
    }
}