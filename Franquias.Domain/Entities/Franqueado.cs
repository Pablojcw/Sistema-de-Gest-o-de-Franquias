namespace Franquias.Domain.Entities;

public class Franqueado
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;
    public string Cnpj { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefone { get; private set; } = string.Empty;

    public Franqueado(
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
    }
}