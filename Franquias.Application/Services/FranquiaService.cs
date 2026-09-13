using Franquias.Application.DTOs.Franquia;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Application.Services.Interfaces;

namespace Franquias.Application.Services;

public class FranquiaService : IFranquiaService
{
    private readonly IFranquiaRepository _repository;

    public FranquiaService(IFranquiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<FranquiaResponse> CriarAsync(CriarFranquiaRequest request)
    {
        var franquia = new Franquia(
            request.Nome,
            request.Cnpj,
            request.Endereco
        );

        var franquiaCriada = await _repository.AdicionarAsync(franquia);

        return new FranquiaResponse
        {
            Id = franquiaCriada.Id,
            Nome = franquiaCriada.Nome,
            Cnpj = franquiaCriada.Cnpj,
            Endereco = franquiaCriada.Endereco,
            Ativa = franquiaCriada.Ativa
        };
    }

    public async Task<FranquiaResponse?> ObterPorIdAsync(Guid id)
    {
        var franquia = await _repository.ObterPorIdAsync(id);

        if (franquia is null)
            return null;

        return new FranquiaResponse
        {
            Id = franquia.Id,
            Nome = franquia.Nome,
            Cnpj = franquia.Cnpj,
            Endereco = franquia.Endereco,
            Ativa = franquia.Ativa
        };
    }

    public async Task<List<FranquiaResponse>> ObterTodasAsync()
    {
        var franquias = await _repository.ObterTodasAsync();

        return franquias.Select(f => new FranquiaResponse
        {
            Id = f.Id,
            Nome = f.Nome,
            Cnpj = f.Cnpj,
            Endereco = f.Endereco,
            Ativa = f.Ativa
        }).ToList();
    }
}