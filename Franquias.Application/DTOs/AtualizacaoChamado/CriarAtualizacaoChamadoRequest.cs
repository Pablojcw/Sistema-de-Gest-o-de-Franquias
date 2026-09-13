namespace Franquias.Application.DTOs.AtualizacaoChamado;

public class CriarAtualizacaoChamadoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }
    public Guid ChamadoId { get; set; }
}