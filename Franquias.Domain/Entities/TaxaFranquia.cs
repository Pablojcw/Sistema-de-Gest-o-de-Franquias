namespace Franquias.Domain.Entities;

public enum StatusTaxaFranquia
{
    Pendente = 1,
    Paga = 2,
    Vencida = 3,
    Isenta = 4
}

public class TaxaFranquia
{
    public Guid Id { get; private set; }

    public decimal Valor { get; private set; }

    public decimal Porcentagem { get; private set; }

    public DateTime DataVencimento { get; private set; }

    public StatusTaxaFranquia Status { get; private set; }

    public Guid UnidadeId { get; private set; }

    public TaxaFranquia(
        decimal valor,
        decimal porcentagem,
        DateTime dataVencimento,
        Guid unidadeId)
    {
        if (valor < 0)
            throw new ArgumentException("O valor nao pode ser negativo");
        else if (porcentagem < 0 || porcentagem > 100)
            throw new ArgumentException("A porcentagem da taxa deve estar entre 0 a 100");

        Id = Guid.NewGuid();
        Valor = valor;
        Porcentagem = porcentagem;
        DataVencimento = dataVencimento;
        UnidadeId = unidadeId;
        Status = StatusTaxaFranquia.Pendente;
    }
}