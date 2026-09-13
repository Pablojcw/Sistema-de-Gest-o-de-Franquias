namespace Franquias.Application.DTOs.Produto;

public class AtualizarProdutoRequest
{
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Categoria { get; set; } = string.Empty;

    public decimal PrecoBase { get; set; }
}