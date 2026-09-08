using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable("Fornecedores");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Nome).IsRequired().HasMaxLength(150);
        builder.Property(f => f.Cnpj).IsRequired().HasMaxLength(14);
        builder.Property(f => f.Email).IsRequired().HasMaxLength(150);
        builder.Property(f => f.Telefone).IsRequired().HasMaxLength(14);
        builder.Property(f => f.Status).IsRequired();
    }
}