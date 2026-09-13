using Franquias.Application.DTOs.Usuario;

namespace Franquias.Application.DTOs.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public UsuarioResponse Usuario { get; set; } = new();
}