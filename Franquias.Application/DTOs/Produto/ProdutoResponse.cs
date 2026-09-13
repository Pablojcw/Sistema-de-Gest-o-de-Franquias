namespace Franquias.Application.DTOs.Produto;
using Franquias.Domain.Entities;

public class ProdutoResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal PrecoBase { get; set; }
    public StatusProduto Status { get; set; }
    public Guid FranquiaId { get; set; }
}