namespace Franquias.Domain.Entities;

public enum TipoMovimentacaoEstoque
{
    Entrada = 1,
    Saida = 2
}

public class MovimentacaoEstoque
{
    public Guid Id { get; private set; }

    public TipoMovimentacaoEstoque Tipo { get; private set; }

    public decimal Quantidade { get; private set; }

    public DateTime Data { get; private set; }

    public Guid ProdutoId { get; private set; }

    public Guid UnidadeId { get; private set; }

    public Guid UsuarioId { get; private set; }

    public MovimentacaoEstoque(
        TipoMovimentacaoEstoque tipo,
        decimal quantidade,
        Guid produtoId,
        Guid unidadeId,
        Guid usuarioId)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        Id = Guid.NewGuid();
        Tipo = tipo;
        Quantidade = quantidade;
        Data = DateTime.UtcNow;
        ProdutoId = produtoId;
        UnidadeId = unidadeId;
        UsuarioId = usuarioId;
    }
}