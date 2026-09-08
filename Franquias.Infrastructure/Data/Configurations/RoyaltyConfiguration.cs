using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Franquias.Infrastructure.Data.Configurations;

public class  RoyaltyConfiguration : IEntityTypeConfiguration<Royalty>
{
    public void Configure(EntityTypeBuilder<Royalty> builder)
    {
        builder.ToTable("Royalties");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Porcentagem).IsRequired().HasPrecision(5,2);
        builder.Property(r => r.Valor).IsRequired().HasPrecision(18, 2);
        builder.Property(r => r.DataInicio).IsRequired();
        builder.Property(r => r.DataFim).IsRequired();
        builder.Property(r => r.DataVencimento).IsRequired();
        builder.Property(r => r.Status).IsRequired();
        builder.HasOne<Unidade>().WithMany().HasForeignKey(r => r.UnidadeId).OnDelete(DeleteBehavior.Restrict);
        
    }
}