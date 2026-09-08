namespace Franquias.Domain.Entities;

public enum StatusVenda
{
    Pendente = 1,
    Concluida = 2,
    Cancelada = 3,
    Estornada = 4
}

public class Venda
{
    public Guid Id { get; private set; }

    public DateTime Data { get; private set; }

    public decimal ValorTotal { get; private set; }

    public StatusVenda Status { get; private set; }

    public Guid UnidadeId { get; private set; }

    public Venda(Guid unidadeId)
    {
        Id = Guid.NewGuid();
        Data = DateTime.UtcNow;
        UnidadeId = unidadeId;
        Status = StatusVenda.Pendente;
        ValorTotal = 0;
    }
}