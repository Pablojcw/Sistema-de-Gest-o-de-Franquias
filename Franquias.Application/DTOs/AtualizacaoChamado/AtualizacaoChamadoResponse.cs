namespace Franquias.Application.DTOs.AtualizacaoChamado;

public class AtualizacaoChamadoResponse
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid ChamadoId { get; set; }
}