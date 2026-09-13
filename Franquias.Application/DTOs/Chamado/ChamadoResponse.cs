using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Chamado;

public class ChamadoResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public StatusChamado Status { get; set; }
    public StatusPrioridade Prioridade { get; set; }
    public DateTime DataAberta { get; set; }
    public DateTime? DataFechamento { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid UnidadeId { get; set; }
}