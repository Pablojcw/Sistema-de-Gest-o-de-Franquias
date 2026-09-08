namespace Franquias.Domain.Entities;

public enum PerfilUsuario
{
    Administrador = 1,
    Franqueado = 2,
    Funcionario = 3
}

public class Usuario
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string SenhaHash { get; private set; } = string.Empty;

    public PerfilUsuario Perfil { get; private set; }

    public bool Ativa { get; private set; }

    public Guid FranquiaId { get; private set; }

    public Guid? UnidadeId { get; private set; }

    public Usuario(
        string nome,
        string email,
        string senhaHash,
        PerfilUsuario perfil,
        Guid franquiaId,
        Guid? unidadeId = null)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Perfil = perfil;
        FranquiaId = franquiaId;
        UnidadeId = unidadeId;
        Ativa = true;
    }
}