using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
{
    public void Configure(EntityTypeBuilder<Estoque> builder)
    {
        builder.ToTable("Estoques");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Quantidade).IsRequired().HasPrecision(18, 2);
        builder.Property(e => e.EstoqueMinimo).IsRequired().HasPrecision(18, 2);
        builder.HasOne<Produto>().WithMany().HasForeignKey(e => e.ProdutoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Unidade>().WithMany().HasForeignKey(e => e.UnidadeId).OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(e => new { e.ProdutoId, e.UnidadeId })
            .IsUnique();
    }
}