namespace Franquias.Application.DTOs.ItemVenda;

public class CriarItemVendaRequest
{
    public decimal Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public Guid VendaId { get; set; }
    public Guid ProdutoId { get; set; }
}