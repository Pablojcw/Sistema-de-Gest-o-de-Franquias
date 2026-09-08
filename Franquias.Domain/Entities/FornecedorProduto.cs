namespace Franquias.Domain.Entities;

public class FornecedorProduto
{
    public Guid FornecedorId { get; private set; }

    public Guid ProdutoId { get; private set; }

    public FornecedorProduto(
        Guid fornecedorId,
        Guid produtoId)
    {
        FornecedorId = fornecedorId;
        ProdutoId = produtoId;
    }
}