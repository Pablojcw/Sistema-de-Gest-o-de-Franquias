using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Usuario;

public class UsuarioResponse
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public PerfilUsuario Perfil { get; set; }

    public bool Ativa { get; set; }

    public Guid FranquiaId { get; set; }

    public Guid? UnidadeId { get; set; }
}