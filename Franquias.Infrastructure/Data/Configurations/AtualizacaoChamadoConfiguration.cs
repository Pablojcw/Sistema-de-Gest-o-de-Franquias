using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class AtualizacaoChamadoConfiguration : IEntityTypeConfiguration<AtualizacaoChamado>
{
    public void Configure(EntityTypeBuilder<AtualizacaoChamado> builder)
    {
        builder.ToTable("AtualizacoesChamado");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Descricao).IsRequired().HasMaxLength(300);
        builder.Property(a => a.Data).IsRequired();

        builder.HasOne<Usuario>().WithMany().HasForeignKey(a => a.UsuarioId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Chamado>().WithMany().HasForeignKey(a => a.ChamadoId).OnDelete(DeleteBehavior.Restrict);
    }
}