using FIAP.Pos.Tech.Challenge.Domain.Entities;

namespace FIAP.Pos.Tech.Challenge.Domain.Interfaces
{
    public interface IPedidoController : IController<Pedido>
    {
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
