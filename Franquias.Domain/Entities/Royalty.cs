namespace Franquias.Domain.Entities;

public enum StatusRoyalty
{
    Pendente = 1,
    Pago = 2,
    Vencido = 3
}

public class Royalty
{
    public Guid Id { get; private set; }

    public decimal Porcentagem { get; private set; }

    public decimal Valor { get; private set; }

    public DateTime DataInicio { get; private set; }

    public DateTime DataFim { get; private set; }

    public DateTime DataVencimento { get; private set; }

    public StatusRoyalty Status { get; private set; }

    public Guid UnidadeId { get; private set; }

    public Royalty(
        decimal porcentagem,
        decimal valor,
        DateTime dataInicio,
        DateTime dataFim,
        DateTime dataVencimento,
        Guid unidadeId)
    {
        
        if(porcentagem < 0 || porcentagem > 100)
            throw new ArgumentException("A porcentagem de royalty deve estar entre 0 e 100");
        else if( valor < 0)
            throw new ArgumentException("O valor de royalty nao pode ser negativo");
        else if(dataInicio > dataFim)
            throw new ArgumentException("A data de inicio nao pode ser posterior a data de fim");
        Id = Guid.NewGuid();

        Porcentagem = porcentagem;
        Valor = valor;
        DataInicio = dataInicio;
        DataFim = dataFim;
        DataVencimento = dataVencimento;
        UnidadeId = unidadeId;
        Status = StatusRoyalty.Pendente;
    }
}