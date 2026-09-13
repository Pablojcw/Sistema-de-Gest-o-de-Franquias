namespace Franquias.Application.DTOs.Relatorios;

public class RelatorioFaturamentoResponse
{
    public Guid UnidadeId { get; set; }

    public string UnidadeNome { get; set; } = string.Empty;

    public decimal Faturamento { get; set; }
}