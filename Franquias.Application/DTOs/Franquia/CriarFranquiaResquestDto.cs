namespace Franquias.Application.DTOs.Franquia;

public class CriarFranquiaRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
}