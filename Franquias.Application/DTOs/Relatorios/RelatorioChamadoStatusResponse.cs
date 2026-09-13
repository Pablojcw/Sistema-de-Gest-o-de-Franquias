using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Relatorios;

public class RelatorioChamadoStatusResponse
{
    public StatusChamado Status { get; set; }

    public int Quantidade { get; set; }
}