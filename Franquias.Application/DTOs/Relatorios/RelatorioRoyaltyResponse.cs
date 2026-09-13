namespace Franquias.Application.DTOs.Relatorios;

public class RelatorioRoyaltyResponse
{
    public Guid UnidadeId { get; set; }

    public string UnidadeNome { get; set; } = string.Empty;

    public decimal Faturamento { get; set; }

    public decimal Porcentagem { get; set; }

    public decimal RoyaltyCalculado { get; set; }
}