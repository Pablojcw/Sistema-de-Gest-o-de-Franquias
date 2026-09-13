using Franquias.Application.DTOs.Comum;
using Franquias.Application.DTOs.Franquia;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

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
        var cnpj = string.Concat(request.Cnpj.Where(char.IsDigit));

        var franquia = new Franquia(
            request.Nome,
            cnpj,
            request.Endereco
        );

        var franquiaCriada = await _repository.AdicionarAsync(franquia);

        return ParaResponse(franquiaCriada);
    }

    public async Task<FranquiaResponse?> ObterPorIdAsync(Guid id)
    {
        var franquia = await _repository.ObterPorIdAsync(id);

        return franquia is null ? null : ParaResponse(franquia);
    }

    public async Task<List<FranquiaResponse>> ObterTodasAsync()
    {
        var franquias = await _repository.ObterTodasAsync();

        return franquias.Select(ParaResponse).ToList();
    }

    public async Task<FranquiaResponse> AtualizarAsync(Guid id, AtualizarFranquiaRequest request)
    {
        var franquia = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Franquia não encontrada.");

        franquia.Atualizar(
            request.Nome,
            request.Endereco);

        await _repository.AtualizarAsync(franquia);

        return ParaResponse(franquia);
    }

    public async Task<FranquiaResponse> AlterarAtivaAsync(Guid id, AlterarAtivoRequest request)
    {
        var franquia = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Franquia não encontrada.");

        franquia.AlterarAtiva(request.Ativo);

        await _repository.AtualizarAsync(franquia);

        return ParaResponse(franquia);
    }

    private static FranquiaResponse ParaResponse(Franquia franquia)
    {
        return new FranquiaResponse
        {
            Id = franquia.Id,
            Nome = franquia.Nome,
            Cnpj = franquia.Cnpj,
            Endereco = franquia.Endereco,
            Ativa = franquia.Ativa
        };
    }
}