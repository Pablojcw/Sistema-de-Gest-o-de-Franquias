using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Royalty;

public class RoyaltyResponse
{
    public Guid Id { get; set; }
    public decimal Porcentagem { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public DateTime DataVencimento { get; set; }
    public StatusRoyalty Status { get; set; }
    public Guid UnidadeId { get; set; }
}