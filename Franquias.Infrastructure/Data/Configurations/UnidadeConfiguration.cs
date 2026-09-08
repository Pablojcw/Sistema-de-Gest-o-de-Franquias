using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
{
    public void Configure(EntityTypeBuilder<Unidade> builder)
    {
        builder.ToTable("Unidades");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Cnpj).IsRequired().HasMaxLength(14);
        builder.Property(u => u.Telefone).IsRequired().HasMaxLength(14);
        builder.Property(u => u.Endereco).IsRequired().HasMaxLength(250);
        builder.Property(u => u.Cidade).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Estado).IsRequired().HasMaxLength(2);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.DataInicio).IsRequired();
        builder.Property(u => u.Situacao).IsRequired();
        //HasOne<BLABLABA> -> a Unidade possui uma franquia
        //WithMany() -> A franquia pode ter muitas Unidade
        //HasForeignKey(u => u.FranquiaId) -> e aonde vamos ter a ligacao da FK
        //OnDelete(DeleteBehavior.Restrict); -> Não vai deixar  apagar a franquia enquanto existir unidades ligadas a ela.
        builder.HasOne<Franquia>().WithMany().HasForeignKey(u => u.FranquiaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Franqueado>().WithMany().HasForeignKey(u => u.FranqueadoId).OnDelete(DeleteBehavior.Restrict);
        

        
    }
}