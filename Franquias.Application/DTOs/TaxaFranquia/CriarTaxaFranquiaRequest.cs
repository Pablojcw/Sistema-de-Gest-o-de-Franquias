namespace Franquias.Application.DTOs.TaxaFranquia;

public class CriarTaxaFranquiaRequest
{
    public decimal Valor { get; set; }
    public decimal Porcentagem { get; set; }
    public DateTime DataVencimento { get; set; }
    public Guid UnidadeId { get; set; }
}