using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.MovimentacaoEstoque;

public class MovimentacaoEstoqueResponse
{
    public Guid Id { get; set; }
    public TipoMovimentacaoEstoque Tipo { get; set; }
    public decimal Quantidade { get; set; }
    public DateTime Data { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid UnidadeId { get; set; }
    public Guid UsuarioId { get; set; }
}