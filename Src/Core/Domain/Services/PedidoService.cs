using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Messages;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.Models.Pedido;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class PedidoService : BaseService<Pedido>, IPedidoService
    {
        protected readonly IGateways<Notificacao> _notificacaoGateway;
        protected readonly IGateways<Dispositivo> _dispositivoGateway;
        protected readonly IGateways<Cliente> _clienteGateway;
        protected readonly IGateways<Produto> _produtoGateway;

        /// <summary>
        /// Lógica de negócio referentes ao pedido.
        /// </summary>
        /// <param name="gateway">Gateway de pedido a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        /// <param name="notificacaoGateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="dispositivoGateway">Gateway de dispositivo a ser injetado durante a execução</param>
        /// <param name="clienteGateway">Gateway de cliente a ser injetado durante a execução</param>
        /// <param name="produtoGateway">Gateway de produto a ser injetado durante a execução</param>
        public PedidoService(IGateways<Pedido> gateway,
            IValidator<Pedido> validator,
            IGateways<Notificacao> notificacaoGateway,
            IGateways<Dispositivo> dispositivoGateway,
            IGateways<Cliente> clienteGateway,
            IGateways<Produto> produtoGateway)
            : base(gateway, validator)
        {
            _notificacaoGateway = notificacaoGateway;
            _dispositivoGateway = dispositivoGateway;
            _clienteGateway = clienteGateway;
            _produtoGateway = produtoGateway;
        }

        /// <summary>
        /// Regra para carregar o pedido e seus itens.
        /// </summary>
        public async override Task<ModelResult> FindByIdAsync(Guid Id)
        {
            var result = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Pedido>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Regras para inserção do pedido
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Pedido entity, string[]? businessRules = null)
        {
            var lstWarnings = new List<string>();

            if (businessRules != null)
                lstWarnings.AddRange(businessRules);

            entity.IdPedido = entity.IdPedido.Equals(default) ? Guid.NewGuid() : entity.IdPedido;

            if (!await _dispositivoGateway.Any(x => ((Dispositivo)x).IdDispositivo.Equals(entity.IdDispositivo)))
                lstWarnings.Add(BusinessMessages.NotFoundInError<Dispositivo>(entity.IdDispositivo));

            if (!await _clienteGateway.Any(x => ((Cliente)x).IdCliente.Equals(entity.IdCliente)))
                lstWarnings.Add(BusinessMessages.NotFoundInError<Cliente>(entity.IdDispositivo));

            entity.DataStatusPedido = entity.Data = DateTime.Now;
            entity.Status = enmPedidoStatus.RECEBIDO.ToString();

            entity.DataStatusPagamento = DateTime.Now;
            entity.StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString();

            foreach (var itemPedido in entity.PedidoItems)
            {
                itemPedido.IdPedido = entity.IdPedido;
                itemPedido.IdPedidoItem = itemPedido.IdPedidoItem.Equals(default) ? Guid.NewGuid() : itemPedido.IdPedidoItem;
                itemPedido.Data = DateTime.Now;
                if (!await _produtoGateway.Any(x => ((Produto)x).IdProduto.Equals(itemPedido.IdProduto)))
                    lstWarnings.Add(BusinessMessages.NotFoundInError<Produto>(entity.IdDispositivo));
            }

            await _gateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido recebido."
            });

            return await base.InsertAsync(entity, lstWarnings.ToArray());
        }

        /// <summary>
        /// Regra para atualização do pedido e suas dependências.
        /// </summary>
        public async override Task<ModelResult> UpdateAsync(Pedido entity, string[]? businessRules = null)
        {
            var dbEntity = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == entity.IdPedido);

            if (dbEntity == null)
                return ModelResultFactory.NotFoundResult<Produto>();

            for (int i = 0; i < dbEntity.PedidoItems.Count; i++)
            {
                var item = dbEntity.PedidoItems.ElementAt(i);
                if (!entity.PedidoItems.Any(x => x.IdPedidoItem.Equals(item.IdPedidoItem)))
                    dbEntity.PedidoItems.Remove(dbEntity.PedidoItems.First(x => x.IdPedidoItem.Equals(item.IdPedidoItem)));
            }

            for (int i = 0; i < entity.PedidoItems.Count; i++)
            {
                var item = entity.PedidoItems.ElementAt(i);
                if (!dbEntity.PedidoItems.Any(x => x.IdPedidoItem.Equals(item.IdPedidoItem)))
                {
                    item.IdPedidoItem = item.IdPedidoItem.Equals(default) ? Guid.NewGuid() : item.IdPedidoItem;
                    dbEntity.PedidoItems.Add(item);
                }
            }

            await _gateway.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Regra para colocar o pedido em preparação.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> IniciarPreparacaoAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _gateway.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.EM_PREPARACAO.ToString();

            var transacao = _gateway.BeginTransaction();
            _notificacaoGateway.UseTransaction(transacao);

            await _gateway.UpdateAsync(entity);
            await _gateway.CommitAsync();

            await _notificacaoGateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido em preparação."
            }); ;
            await _notificacaoGateway.CommitAsync();

            await transacao.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Regra para colocar o pedido pronto.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> FinalizarPreparacaoAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _gateway.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.PRONTO.ToString();

            var transacao = _gateway.BeginTransaction();
            _notificacaoGateway.UseTransaction(transacao);

            await _gateway.UpdateAsync(entity);
            await _gateway.CommitAsync();

            await _notificacaoGateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido pronto."
            });
            await _notificacaoGateway.CommitAsync();

            await transacao.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Regra para colocar o pedido finalizado.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> FinalizarAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _gateway.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.FINALIZADO.ToString();

            await _gateway.UpdateAsync(entity);
            await _gateway.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Regra para retornar os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        public async ValueTask<PagingQueryResult<Pedido>> GetListaAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Desc";
            return await _gateway.GetItemsAsync(filter, x => x.Status != enmPedidoStatus.FINALIZADO.ToString(), o => o.Data);
        }


        /// <summary>
        /// Regra para para consultar o pagamento de um pedido.
        /// </summary>
        public async Task<ModelResult> ConsultarPagamentoAsync(Guid id)
        {
            var result = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Pedido>();

            return ModelResultFactory.SucessResult(result.StatusPagamento);
        }

        /// <summary>
        ///  Regra de Webhook para notificação de pagamento.
        /// </summary>
        public async Task<ModelResult> WebhookPagamento(WebhookPagamento webhook, string[]? businessRules)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _gateway.FindByIdAsync(webhook.IdPedido);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.DataStatusPagamento = DateTime.Now;
            entity.StatusPagamento = webhook.StatusPagamento;

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            await _gateway.UpdateAsync(entity);
            await _gateway.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);
        }
    }
}
