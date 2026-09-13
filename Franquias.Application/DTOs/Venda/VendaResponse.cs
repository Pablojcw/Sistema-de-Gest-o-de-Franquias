using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Venda;

public class VendaResponse
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public Guid UnidadeId { get; set; }
    public StatusVenda Status { get; set; }
    public decimal ValorTotal { get; set; }
}