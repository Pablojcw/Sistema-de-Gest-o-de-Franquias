using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Franquias.Application.DTOs.Auth;
using Franquias.Application.DTOs.Usuario;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Franquias.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

        if (usuario is null ||
            !usuario.Ativa ||
            !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
        {
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, usuario.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, usuario.Perfil.ToString())
        };

        var secret = _configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("Jwt:Secret não configurado.");
        var issuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt:Issuer não configurado.");
        var audience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt:Audience não configurado.");
        var horas = int.Parse(_configuration["Jwt:ExpiracaoEmHoras"] ?? "8");

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(horas),
            signingCredentials: credenciais);

        var tokenEscrito = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponse
        {
            Token = tokenEscrito,
            Usuario = new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Ativa = usuario.Ativa,
                FranquiaId = usuario.FranquiaId,
                UnidadeId = usuario.UnidadeId
            }
        };
    }
}