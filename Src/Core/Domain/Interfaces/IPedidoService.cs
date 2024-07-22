using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.Models.Pedido;

namespace FIAP.Pos.Tech.Challenge.Domain.Interfaces
{
    public interface IPedidoService : IService<Pedido>
    {
        /// <summary>
        /// Pedido em preparação.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> IniciarPreparacaoAsync(Guid id, string[]? businessRules = null);

        /// <summary>
        /// Pedido pronto.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> FinalizarPreparacaoAsync(Guid id, string[]? businessRules = null);

        /// <summary>
        /// Pedido finalizado.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> FinalizarAsync(Guid id, string[]? businessRules = null);

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        ValueTask<PagingQueryResult<Pedido>> GetListaAsync(IPagingQueryParam filter);

        /// <summary>
        ///  Webhook para notificação de pagamento.
        /// </summary>
        Task<ModelResult> WebhookPagamento(WebhookPagamento entity, string[]? businessRules);
    }
}
