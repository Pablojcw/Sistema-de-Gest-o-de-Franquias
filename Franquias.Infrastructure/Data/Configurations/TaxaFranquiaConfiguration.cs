using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class TaxaFranquiaConfiguration : IEntityTypeConfiguration<TaxaFranquia>
{
    public void Configure(EntityTypeBuilder<TaxaFranquia> builder)
    {
        builder.ToTable("TaxasFranquia");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Valor).IsRequired().HasPrecision(18, 2);
        builder.Property(f => f.Porcentagem).IsRequired().HasPrecision(5, 2);
        builder.Property(f => f.DataVencimento).IsRequired();
        builder.Property(f => f.Status).IsRequired();
        builder.HasOne<Unidade>().WithMany().HasForeignKey(f => f.UnidadeId).OnDelete(DeleteBehavior.Restrict);
    }
}