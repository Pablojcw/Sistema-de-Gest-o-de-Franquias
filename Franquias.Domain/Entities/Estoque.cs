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

    public void Creditar(decimal quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        Quantidade += quantidade;
    }

    public void Debitar(decimal quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (quantidade > Quantidade)
            throw new InvalidOperationException(
                $"Saldo insuficiente no estoque. Saldo atual: {Quantidade}, necessário: {quantidade}.");

        Quantidade -= quantidade;
    }

    public void AlterarEstoqueMinimo(decimal estoqueMinimo)
    {
        if (estoqueMinimo < 0)
            throw new ArgumentException("O estoque mínimo não pode ser negativo.");

        EstoqueMinimo = estoqueMinimo;
    }

    public bool EstaAbaixoDoMinimo() => Quantidade <= EstoqueMinimo;
}