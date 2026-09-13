using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Usuario;

public class AtualizarUsuarioRequest
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public PerfilUsuario Perfil { get; set; }

    public Guid? UnidadeId { get; set; }
}