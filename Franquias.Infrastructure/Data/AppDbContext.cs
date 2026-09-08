using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Franquia> Franquias => Set<Franquia>();
    public DbSet<Franqueado> Franqueados => Set<Franqueado>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Estoque> Estoques => Set<Estoque>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();
    public DbSet<Venda> Vendas => Set<Venda>();
    public DbSet<ItemVenda> ItensVenda => Set<ItemVenda>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    public DbSet<FornecedorProduto> FornecedorProdutos => Set<FornecedorProduto>();
    public DbSet<TaxaFranquia> TaxasFranquia => Set<TaxaFranquia>();
    public DbSet<Royalty> Royalties => Set<Royalty>();
    public DbSet<Chamado> Chamados => Set<Chamado>();
    public DbSet<AtualizacaoChamado> AtualizacoesChamado => Set<AtualizacaoChamado>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        
    }
}