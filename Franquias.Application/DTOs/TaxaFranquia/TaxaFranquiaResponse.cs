using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.TaxaFranquia;

public class TaxaFranquiaResponse
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public decimal Porcentagem { get; set; }
    public DateTime DataVencimento { get; set; }
    public StatusTaxaFranquia Status { get; set; }
    public Guid UnidadeId { get; set; }
}