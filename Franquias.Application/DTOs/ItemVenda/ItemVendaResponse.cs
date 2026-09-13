namespace Franquias.Application.DTOs.ItemVenda;

public class ItemVendaResponse
{
    public Guid Id { get; set; }
    public decimal Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal SubTotal { get; set; }
    public Guid VendaId { get; set; }
    public Guid ProdutoId { get; set; }
}