using Franquias.Application.DTOs.Franqueado;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;

namespace Franquias.Application.Services;

public class FranqueadoService : IFranqueadoService
{
    private readonly IFranqueadoRepository _repository;

    public FranqueadoService(IFranqueadoRepository repository)
    {
        _repository = repository;
    }

    public async Task<FranqueadoResponse> CriarAsync(CriarFranqueadoRequest request)
    {
        var cnpj = string.Concat(request.Cnpj.Where(char.IsDigit));

        var franqueado = new Franqueado(
            request.Nome,
            cnpj,
            request.Email,
            request.Telefone
        );

        var franqueadoCriado = await _repository.AdicionarAsync(franqueado);

        return ParaResponse(franqueadoCriado);
    }

    public async Task<FranqueadoResponse?> ObterPorIdAsync(Guid id)
    {
        var franqueado = await _repository.ObterPorIdAsync(id);

        return franqueado is null ? null : ParaResponse(franqueado);
    }

    public async Task<List<FranqueadoResponse>> ObterTodasAsync()
    {
        var franqueados = await _repository.ObterTodasAsync();

        return franqueados.Select(ParaResponse).ToList();
    }

    public async Task<FranqueadoResponse> AtualizarAsync(Guid id, AtualizarFranqueadoRequest request)
    {
        var franqueado = await _repository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Franqueado não encontrado.");

        var cnpj = string.Concat(request.Cnpj.Where(char.IsDigit));

        franqueado.Atualizar(
            request.Nome,
            cnpj,
            request.Email,
            request.Telefone);

        await _repository.AtualizarAsync(franqueado);

        return ParaResponse(franqueado);
    }

    private static FranqueadoResponse ParaResponse(Franqueado franqueado)
    {
        return new FranqueadoResponse
        {
            Id = franqueado.Id,
            Nome = franqueado.Nome,
            Cnpj = franqueado.Cnpj,
            Email = franqueado.Email,
            Telefone = franqueado.Telefone
        };
    }
}