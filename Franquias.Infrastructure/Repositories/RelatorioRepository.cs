using Franquias.Application.DTOs.Relatorios;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly AppDbContext _context;

    public RelatorioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RelatorioFaturamentoResponse>> ObterFaturamentoAsync(
        Guid? unidadeId,
        DateTime dataInicio,
        DateTime dataFim)
    {
        var vendas = _context.Vendas.AsNoTracking()
            .Where(v => v.Status == StatusVenda.Concluida &&
                        v.Data >= dataInicio &&
                        v.Data <= dataFim);

        if (unidadeId.HasValue)
            vendas = vendas.Where(v => v.UnidadeId == unidadeId.Value);

        return await vendas
            .Join(
                _context.Unidades.AsNoTracking(),
                v => v.UnidadeId,
                u => u.Id,
                (v, u) => new { v.UnidadeId, u.Nome, v.ValorTotal })
            .GroupBy(x => new { x.UnidadeId, x.Nome })
            .Select(g => new RelatorioFaturamentoResponse
            {
                UnidadeId = g.Key.UnidadeId,
                UnidadeNome = g.Key.Nome,
                Faturamento = g.Sum(x => x.ValorTotal)
            })
            .OrderByDescending(r => r.Faturamento)
            .ToListAsync();
    }

    public async Task<decimal> ObterTotalVendasAsync(
        Guid unidadeId,
        DateTime dataInicio,
        DateTime dataFim)
    {
        return await _context.Vendas.AsNoTracking()
            .Where(v => v.UnidadeId == unidadeId &&
                        v.Status == StatusVenda.Concluida &&
                        v.Data >= dataInicio &&
                        v.Data <= dataFim)
            .SumAsync(v => (decimal?)v.ValorTotal) ?? 0;
    }

    public async Task<decimal?> ObterPorcentagemRoyaltyAsync(Guid unidadeId)
    {
        var royalty = await _context.Royalties.AsNoTracking()
            .Where(r => r.UnidadeId == unidadeId)
            .OrderByDescending(r => r.DataFim)
            .FirstOrDefaultAsync();

        return royalty?.Porcentagem;
    }

    public async Task<decimal> ObterRoyaltiesGeradosAsync(
        Guid? unidadeId,
        DateTime? dataInicio,
        DateTime? dataFim)
    {
        var query = _context.Royalties.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            query = query.Where(r => r.UnidadeId == unidadeId.Value);

        if (dataInicio.HasValue)
            query = query.Where(r => r.DataFim >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(r => r.DataInicio <= dataFim.Value);

        return await query.SumAsync(r => (decimal?)r.Valor) ?? 0;
    }

    public async Task<List<RelatorioProdutoMaisVendidoResponse>> ObterProdutosMaisVendidosAsync(
        DateTime dataInicio,
        DateTime dataFim,
        int top)
    {
        var query =
            from v in _context.Vendas.AsNoTracking()
            join i in _context.ItensVenda.AsNoTracking() on v.Id equals i.VendaId
            join p in _context.Produtos.AsNoTracking() on i.ProdutoId equals p.Id
            where v.Status == StatusVenda.Concluida &&
                  v.Data >= dataInicio &&
                  v.Data <= dataFim
            group new { i } by new { i.ProdutoId, p.Nome } into g
            select new RelatorioProdutoMaisVendidoResponse
            {
                ProdutoId = g.Key.ProdutoId,
                ProdutoNome = g.Key.Nome,
                Quantidade = g.Sum(x => x.i.Quantidade),
                Receita = g.Sum(x => x.i.SubTotal)
            };

        return await query
            .OrderByDescending(r => r.Quantidade)
            .Take(top)
            .ToListAsync();
    }

    public async Task<List<RelatorioEstoqueCriticoResponse>> ObterEstoqueCriticoAsync(Guid? unidadeId)
    {
        var estoques = _context.Estoques.AsNoTracking()
            .Where(e => e.Quantidade <= e.EstoqueMinimo);

        if (unidadeId.HasValue)
            estoques = estoques.Where(e => e.UnidadeId == unidadeId.Value);

        var query =
            from e in estoques
            join u in _context.Unidades.AsNoTracking() on e.UnidadeId equals u.Id
            join p in _context.Produtos.AsNoTracking() on e.ProdutoId equals p.Id
            select new RelatorioEstoqueCriticoResponse
            {
                UnidadeId = u.Id,
                UnidadeNome = u.Nome,
                ProdutoId = p.Id,
                ProdutoNome = p.Nome,
                Quantidade = e.Quantidade,
                EstoqueMinimo = e.EstoqueMinimo
            };

        return await query
            .OrderBy(r => r.UnidadeNome)
            .ThenBy(r => r.ProdutoNome)
            .ToListAsync();
    }

    public async Task<List<RelatorioChamadoStatusResponse>> ObterChamadosPorStatusAsync(Guid? unidadeId)
    {
        var chamados = _context.Chamados.AsNoTracking().AsQueryable();

        if (unidadeId.HasValue)
            chamados = chamados.Where(c => c.UnidadeId == unidadeId.Value);

        return await chamados
            .GroupBy(c => c.Status)
            .Select(g => new RelatorioChamadoStatusResponse
            {
                Status = g.Key,
                Quantidade = g.Count()
            })
            .ToListAsync();
    }
}