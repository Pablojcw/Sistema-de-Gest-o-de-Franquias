namespace Franquias.Domain.Entities;

public class Unidade
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cnpj { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefone { get; private set; } = string.Empty;
    public string Endereco { get; private set; } = string.Empty;
    public string Cidade { get; private set; } = string.Empty;
    public string Estado { get; private set; } = string.Empty;
    public DateTime DataInicio { get; private set; }
    public string Situacao { get; private set; } = string.Empty;
    public Guid FranquiaId { get; private set; }
    public Guid FranqueadoId { get; private set; }

    public Unidade(
        string nome,
        string cnpj,
        string endereco,
        string cidade,
        string estado,
        string email,
        string telefone,
        DateTime dataInicio,
        Guid franqueadoId,
        Guid franquiaId)
    {
        Id = Guid.NewGuid();

        Nome = nome;
        Cnpj = cnpj;
        Endereco = endereco;
        Cidade = cidade;
        Estado = estado;
        Email = email;
        Telefone = telefone;
        DataInicio = dataInicio;
        FranqueadoId = franqueadoId;
        FranquiaId = franquiaId;
        Situacao = "Ativa";
    }
}