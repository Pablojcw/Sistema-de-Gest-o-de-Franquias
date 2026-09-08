namespace Franquias.Domain.Entities;

public enum StatusChamado
{
    Pendente = 1,
    Aguardando = 2,
    Executando = 3,
    Terminado = 4
}

public enum StatusPrioridade
{
    Baixa = 1,
    Media = 2,
    Alta = 3
}

public class Chamado
{
    public Guid Id { get; private set; }

    public string Titulo { get; private set; } = string.Empty;

    public string Descricao { get; private set; } = string.Empty;

    public string Categoria { get; private set; } = string.Empty;

    public StatusChamado Status { get; private set; }

    public StatusPrioridade Prioridade { get; private set; }

    public DateTime DataAberta { get; private set; }

    public DateTime? DataFechamento { get; private set; }

    public Guid UsuarioId { get; private set; }

    public Guid UnidadeId { get; private set; }

    public Chamado(
        string titulo,
        string descricao,
        string categoria,
        Guid usuarioId,
        Guid unidadeId)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Categoria = categoria;
        UsuarioId = usuarioId;
        UnidadeId = unidadeId;
        DataAberta = DateTime.UtcNow;
        Status = StatusChamado.Aguardando;
        Prioridade = StatusPrioridade.Baixa;
    }
}