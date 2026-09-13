using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Chamado;

public class AtualizarStatusChamadoRequest
{
    public StatusChamado Status { get; set; }
}