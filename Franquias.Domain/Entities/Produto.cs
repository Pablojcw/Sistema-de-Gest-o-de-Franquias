namespace Franquias.Domain.Entities;

public enum StatusProduto
{
    Ativo = 1,
    Inativo = 2
}

public class Produto
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Descricao { get; private set; } = string.Empty;

    public string Categoria { get; private set; } = string.Empty;

    public decimal PrecoBase { get; private set; }

    public StatusProduto Status { get; private set; }

    public Guid FranquiaId { get; private set; }

    public Produto(
        string nome,
        string descricao,
        string categoria,
        decimal precoBase,
        Guid franquiaId)
    {
        if(precoBase < 0)
            throw new ArgumentException("O preco base nao pode ser negativo");
            
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Categoria = categoria;
        PrecoBase = precoBase;
        FranquiaId = franquiaId;
        Status = StatusProduto.Ativo;
    }
}