namespace Franquias.Application.DTOs.Relatorios;

public class RelatorioProdutoMaisVendidoResponse
{
    public Guid ProdutoId { get; set; }

    public string ProdutoNome { get; set; } = string.Empty;

    public decimal Quantidade { get; set; }

    public decimal Receita { get; set; }
}