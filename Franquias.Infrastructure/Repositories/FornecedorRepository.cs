using Franquias.Application.DTOs.Comum;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Fornecedor> AdicionarAsync(Fornecedor fornecedor)
    {
        await _context.Fornecedores.AddAsync(fornecedor);
        await _context.SaveChangesAsync();

        return fornecedor;
    }

    public async Task<Fornecedor?> ObterPorIdAsync(Guid id)
    {
        return await _context.Fornecedores
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<ResultadoPaginado<Fornecedor>> ObterFiltradasAsync(
        string? nome = null,
        string? cnpj = null,
        StatusFornecedor? status = null,
        int pagina = 1,
        int tamanhoPagina = 20)
    {
        if (pagina < 1)
            pagina = 1;
        if (tamanhoPagina < 1)
            tamanhoPagina = 20;

        var query = _context.Fornecedores.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(f => f.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(cnpj))
            query = query.Where(f => f.Cnpj == cnpj);

        if (status.HasValue)
            query = query.Where(f => f.Status == status.Value);

        var total = await query.CountAsync();

        var itens = await query
            .OrderBy(f => f.Nome)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new ResultadoPaginado<Fornecedor>
        {
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = total,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            Itens = itens
        };
    }

    public async Task<Fornecedor> AtualizarAsync(Fornecedor fornecedor)
    {
        _context.Update(fornecedor);
        await _context.SaveChangesAsync();

        return fornecedor;
    }
}