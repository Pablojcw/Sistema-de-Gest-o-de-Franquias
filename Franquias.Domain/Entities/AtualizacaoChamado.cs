namespace Franquias.Domain.Entities;

public class AtualizacaoChamado
{
    public Guid Id { get; private set; }

    public string Descricao { get; private set; } = string.Empty;

    public DateTime Data { get; private set; }

    public Guid UsuarioId { get; private set; }

    public Guid ChamadoId { get; private set; }

    public AtualizacaoChamado(
        string descricao,
        Guid usuarioId,
        Guid chamadoId)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição da atualização é obrigatória.");

        Id = Guid.NewGuid();
        Descricao = descricao;
        Data = DateTime.UtcNow;
        UsuarioId = usuarioId;
        ChamadoId = chamadoId;
    }
}