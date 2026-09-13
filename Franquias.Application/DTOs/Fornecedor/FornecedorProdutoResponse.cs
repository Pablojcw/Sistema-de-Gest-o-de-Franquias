namespace Franquias.Application.DTOs.Fornecedor;

public class FornecedorProdutoResponse
{
    public Guid FornecedorId { get; set; }

    public string FornecedorNome { get; set; } = string.Empty;

    public Guid ProdutoId { get; set; }

    public string ProdutoNome { get; set; } = string.Empty;
}