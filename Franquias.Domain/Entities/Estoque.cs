namespace Franquias.Domain.Entities;

public class Estoque
{
    public Guid Id { get; private set; }

    public decimal Quantidade { get; private set; }

    public decimal EstoqueMinimo { get; private set; }

    public Guid ProdutoId { get; private set; }

    public Guid UnidadeId { get; private set; }

    public Estoque(
        decimal quantidade,
        decimal estoqueMinimo,
        Guid produtoId,
        Guid unidadeId)
    {
         if (quantidade < 0)
        throw new ArgumentException("A quantidade em estoque não pode ser negativa.");

    if (estoqueMinimo < 0)
        throw new ArgumentException("O estoque mínimo não pode ser negativo.");
        
        Id = Guid.NewGuid();
        Quantidade = quantidade;
        EstoqueMinimo = estoqueMinimo;
        ProdutoId = produtoId;
        UnidadeId = unidadeId;
    }
}