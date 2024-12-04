using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.ValuesObject;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Interfaces
{
    public interface IPedidoController : IController<Entities.Pedido>
    {
        /// <summary>
        /// Alterar o status de pagamento do pedido
        /// </summary>
        Task<ModelResult> AlterarStatusPagamento(Guid id, enmPedidoStatusPagamento statusPagamento);

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        Task<PagingQueryResult<Entities.Pedido>> GetListaAsync(PagingQueryParam<Entities.Pedido> param);

    }
}
