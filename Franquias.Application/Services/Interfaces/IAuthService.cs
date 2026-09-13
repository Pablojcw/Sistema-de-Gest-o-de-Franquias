using Franquias.Application.DTOs.Auth;

namespace Franquias.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}