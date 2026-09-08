namespace Franquias.Domain.Entities;

public class Franquia
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Cnpj { get; private set; } = string.Empty;

    public string Endereco { get; private set; } = string.Empty;

    public bool Ativa { get; private set; }

    public Franquia(
        string nome,
        string cnpj,
        string endereco)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Cnpj = cnpj;
        Endereco = endereco;
        Ativa = true;
    }
}