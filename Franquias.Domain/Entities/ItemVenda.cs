namespace Franquias.Domain.Entities;

public class ItemVenda
{
    public Guid Id { get; private set; }

    public decimal Quantidade { get; private set; }

    public decimal PrecoUnitario { get; private set; }

    public decimal SubTotal { get; private set; }

    public Guid VendaId { get; private set; }

    public Guid ProdutoId { get; private set; }

    public ItemVenda(
        decimal quantidade,
        decimal precoUnitario,
        Guid vendaId,
        Guid produtoId)
    {
        if (quantidade <= 0)
            throw new ArgumentException(
                "A quantidade deve ser maior que zero.");

        if (precoUnitario < 0)
            throw new ArgumentException(
                "O preço unitário não pode ser negativo.");

        Id = Guid.NewGuid();

        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        SubTotal = quantidade * precoUnitario;

        VendaId = vendaId;
        ProdutoId = produtoId;
    }
}