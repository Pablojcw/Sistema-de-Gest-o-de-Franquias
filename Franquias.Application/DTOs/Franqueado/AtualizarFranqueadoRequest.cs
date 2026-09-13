namespace Franquias.Application.DTOs.Franqueado;

public class AtualizarFranqueadoRequest
{
    public string Nome { get; set; } = string.Empty;

    public string Cnpj { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;
}