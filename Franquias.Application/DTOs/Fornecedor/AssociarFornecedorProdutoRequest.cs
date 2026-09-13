namespace Franquias.Application.DTOs.Fornecedor;

public class AssociarFornecedorProdutoRequest
{
    public Guid FornecedorId { get; set; }

    public Guid ProdutoId { get; set; }
}