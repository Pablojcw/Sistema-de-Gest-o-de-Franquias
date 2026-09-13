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

    public void Confirmar(decimal valorTotal)
    {
        if (Status != StatusVenda.Pendente)
            throw new InvalidOperationException(
                "Somente vendas pendentes podem ser confirmadas.");

        if (valorTotal <= 0)
            throw new ArgumentException(
                "A venda precisa possuir pelo menos um item para ser confirmada.");

        ValorTotal = valorTotal;
        Status = StatusVenda.Concluida;
    }

    public void Cancelar()
    {
        if (Status == StatusVenda.Concluida || Status == StatusVenda.Cancelada)
            throw new InvalidOperationException("Esta venda não pode ser cancelada.");

        Status = StatusVenda.Cancelada;
    }
}