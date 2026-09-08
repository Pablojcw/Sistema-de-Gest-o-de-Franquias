using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franquias.Infrastructure.Data.Configurations;

public class FornecedorProdutoConfiguration : IEntityTypeConfiguration<FornecedorProduto>
{
    public void Configure(EntityTypeBuilder<FornecedorProduto> builder)
    {
        builder.ToTable("FornecedorProdutos");

        builder.HasKey(fp => new
        {
            fp.FornecedorId,
            fp.ProdutoId
        });

        builder.HasOne<Fornecedor>().WithMany().HasForeignKey(fp => fp.FornecedorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Produto>().WithMany().HasForeignKey(fp => fp.ProdutoId).OnDelete(DeleteBehavior.Restrict);
    }
}