using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Chamado;

public class CriarChamadoRequest
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public StatusPrioridade Prioridade { get; set; }
    public Guid UnidadeId { get; set; }
}