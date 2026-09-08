using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Nome).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.SenhaHash).IsRequired().HasMaxLength(255);
        builder.Property(u => u.Ativa).IsRequired();
        builder.Property(u => u.Perfil).IsRequired();
        builder.HasOne<Franquia>().WithMany().HasForeignKey(u => u.FranquiaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Unidade>().WithMany().HasForeignKey(u => u.UnidadeId).OnDelete(DeleteBehavior.Restrict);
    }
}