namespace Franquias.Application.DTOs.Relatorios;

public class RelatorioRankingResponse
{
    public int Posicao { get; set; }

    public Guid UnidadeId { get; set; }

    public string UnidadeNome { get; set; } = string.Empty;

    public decimal Faturamento { get; set; }
}