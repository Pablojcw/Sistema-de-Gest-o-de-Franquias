namespace Franquias.Application.DTOs.Franquia;

public class FranquiaResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public bool Ativa { get; set; }
}