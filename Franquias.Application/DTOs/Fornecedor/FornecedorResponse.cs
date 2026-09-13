using Franquias.Domain.Entities;

namespace Franquias.Application.DTOs.Fornecedor;

public class FornecedorResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public StatusFornecedor Status { get; set; }
}