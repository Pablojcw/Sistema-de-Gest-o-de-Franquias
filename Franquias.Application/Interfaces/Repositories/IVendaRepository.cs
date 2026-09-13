using Franquias.Application.DTOs.Comum;
using Franquias.Domain.Entities;

namespace Franquias.Application.Interfaces.Repositories;

public interface IVendaRepository
{
    Task<Venda> AdicionarAsync(Venda venda);

    Task<Venda?> ObterPorIdAsync(Guid id);

    Task<ResultadoPaginado<Venda>> ObterFiltradasAsync(
        Guid? unidadeId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int pagina = 1,
        int tamanhoPagina = 20);

    Task<Venda> AtualizarAsync(Venda venda);
}