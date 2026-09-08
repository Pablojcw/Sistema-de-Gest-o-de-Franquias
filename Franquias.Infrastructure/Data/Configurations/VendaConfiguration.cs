using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.ToTable("Vendas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.ValorTotal).IsRequired().HasPrecision(18, 2);
        builder.Property(v => v.Data).IsRequired();
        builder.Property(v => v.Status).IsRequired();
         builder.HasOne<Unidade>().WithMany().HasForeignKey(v => v.UnidadeId).OnDelete(DeleteBehavior.Restrict);
    }
}