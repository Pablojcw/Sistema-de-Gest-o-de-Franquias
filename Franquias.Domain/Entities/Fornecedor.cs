namespace Franquias.Domain.Entities;

public enum StatusFornecedor
{
    Ativo = 1,
    Inativo = 2
}

public class Fornecedor
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Cnpj { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string Telefone { get; private set; } = string.Empty;

    public StatusFornecedor Status { get; private set; }

    public Fornecedor(
        string nome,
        string cnpj,
        string email,
        string telefone)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Cnpj = cnpj;
        Email = email;
        Telefone = telefone;
        Status = StatusFornecedor.Ativo;
    }
}