using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IMovimentacaoEstoqueRepository
{
    Task<MovimentacaoEstoque> AdicionarAsync(MovimentacaoEstoque movimentacaoEstoque);

    Task<MovimentacaoEstoque?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<MovimentacaoEstoque>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        Guid? produtoId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20);
}