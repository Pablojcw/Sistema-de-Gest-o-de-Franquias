using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class ItemVendaConfiguration : IEntityTypeConfiguration<ItemVenda>
{
    public void Configure(EntityTypeBuilder<ItemVenda> builder)
    {
        builder.ToTable("ItemVendas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.PrecoUnitario).IsRequired().HasPrecision(18,2);
        builder.Property(v => v.Quantidade).IsRequired().HasPrecision(18,2);
        builder.Property(v => v.SubTotal).IsRequired().HasPrecision(18, 2);
        builder.HasOne<Venda>().WithMany().HasForeignKey(v =>v.VendaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Produto>().WithMany().HasForeignKey(v => v.ProdutoId).OnDelete(DeleteBehavior.Restrict);
    }
}
