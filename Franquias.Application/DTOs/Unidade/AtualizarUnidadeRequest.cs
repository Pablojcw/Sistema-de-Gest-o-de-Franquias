namespace Franquias.Application.DTOs.Unidade;

public class AtualizarUnidadeRequest
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Endereco { get; set; } = string.Empty;

    public string Cidade { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;
}