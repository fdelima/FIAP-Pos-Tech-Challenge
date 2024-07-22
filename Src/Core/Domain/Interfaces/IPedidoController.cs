using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.Models.MercadoPago;
using FIAP.Pos.Tech.Challenge.Domain.Models.Pedido;
using Microsoft.AspNetCore.Http;

namespace FIAP.Pos.Tech.Challenge.Domain.Interfaces
{
    public interface IPedidoController : IController<Pedido>
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
                
        /// <summary>
        ///  Webhook para notificação de pagamento.
        /// </summary>
        Task<ModelResult> WebhookPagamento(WebhookPagamento notificacao, IHeaderDictionary headers);

        /// <summary>
        ///  Mercado pago recebimento de notificação webhook.
        ///  https://www.mercadopago.com.br/developers/pt/docs/your-integrations/notifications/webhooks#editor_13
        /// </summary>
        Task<ModelResult> MercadoPagoWebhoock(MercadoPagoWebhoock notificacao);

    }
}
