namespace Franquias.Application.DTOs.Estoque;

public class CriarEstoqueRequest
{
    public decimal Quantidade { get; set; }
    public decimal EstoqueMinimo { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid UnidadeId { get; set; }
}