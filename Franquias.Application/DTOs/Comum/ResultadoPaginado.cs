namespace Franquias.Application.DTOs.Comum;

public class ResultadoPaginado<T>
{
    public int Pagina { get; set; }

    public int TamanhoPagina { get; set; }

    public int TotalItens { get; set; }

    public int TotalPaginas { get; set; }

    public List<T> Itens { get; set; } = [];
}