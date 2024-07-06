using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Domain.Interfaces
{
    public interface IPedidoAppService : IController<Pedido>
    {
        /// <summary>
        /// Pedido em preparação.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> IniciarPreparacaoAsync(Guid id);

        /// <summary>
        /// Pedido pronto.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> FinalizarPreparacaoAsync(Guid id);

        /// <summary>
        /// Pedido finalizado.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> FinalizarAsync(Guid id);

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        Task<PagingQueryResult<Pedido>> GetListaAsync(PagingQueryParam<Pedido> param);
    }
}
