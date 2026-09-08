using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class FranquiaConfiguration : IEntityTypeConfiguration<Franquia>
{
    public void Configure(EntityTypeBuilder<Franquia> builder)
    {
        builder.ToTable("Franquias");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Nome).IsRequired().HasMaxLength(150);
        builder.Property(f => f.Cnpj).IsRequired().HasMaxLength(14);
        builder.Property(f => f.Endereco).IsRequired().HasMaxLength(250);
        builder.Property(f => f.Ativa).IsRequired();
    }
}