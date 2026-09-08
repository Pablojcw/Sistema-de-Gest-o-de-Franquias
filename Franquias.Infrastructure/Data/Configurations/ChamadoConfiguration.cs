using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class ChamadoConfiguration : IEntityTypeConfiguration<Chamado>
{
    public void Configure(EntityTypeBuilder<Chamado> builder)
    {
        builder.ToTable("Chamados");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Titulo).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Categoria).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Status).IsRequired();
        builder.Property(c => c.Prioridade).IsRequired();
        builder.Property(c => c.DataAberta).IsRequired();
        builder.Property(c => c.DataFechamento).IsRequired(false);

        builder.HasOne<Usuario>().WithMany().HasForeignKey(c => c.UsuarioId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Unidade>().WithMany().HasForeignKey(c => c.UnidadeId).OnDelete(DeleteBehavior.Restrict);

    }
}
