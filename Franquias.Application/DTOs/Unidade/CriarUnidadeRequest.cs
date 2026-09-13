namespace Franquias.Application.DTOs.Unidade;

public class CriarUnidadeRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }

    public Guid FranquiaId { get; set; }
    public Guid FranqueadoId { get; set; }
}