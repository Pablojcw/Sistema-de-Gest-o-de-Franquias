namespace Franquias.Application.DTOs.Royalty;

public class CriarRoyaltyRequest
{
    public decimal Porcentagem { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public DateTime DataVencimento { get; set; }
    public Guid UnidadeId { get; set; }
}