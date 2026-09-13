namespace Franquias.Application.DTOs.Relatorios;

public class RelatorioEstoqueCriticoResponse
{
    public Guid UnidadeId { get; set; }

    public string UnidadeNome { get; set; } = string.Empty;

    public Guid ProdutoId { get; set; }

    public string ProdutoNome { get; set; } = string.Empty;

    public decimal Quantidade { get; set; }

    public decimal EstoqueMinimo { get; set; }
}