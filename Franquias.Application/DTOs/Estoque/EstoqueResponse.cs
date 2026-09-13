namespace Franquias.Application.DTOs.Estoque;

public class EstoqueResponse
{
    public Guid Id { get; set; }
    public decimal Quantidade { get; set; }
    public decimal EstoqueMinimo { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid UnidadeId { get; set; }
}