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
        protected readonly IGateways<Notificacao> _notificacaoRepository;
        protected readonly IGateways<Dispositivo> _dispositivoRepository;
        protected readonly IGateways<Cliente> _clienteRepository;
        protected readonly IGateways<Produto> _produtoRepository;

        public PedidoService(IGateways<Pedido> repository,
            IValidator<Pedido> validator,
            IGateways<Notificacao> NotificacaoRepository,
            IGateways<Dispositivo> DispositivoRepository,
            IGateways<Cliente> ClienteRepository,
            IGateways<Produto> ProdutoRepository)
            : base(repository, validator)
        {
            _notificacaoRepository = NotificacaoRepository;
            _dispositivoRepository = DispositivoRepository;
            _clienteRepository = ClienteRepository;
            _produtoRepository = ProdutoRepository;
        }

        /// <summary>
        /// Carrega o pedido e seus itens.
        /// </summary>
        public async override Task<ModelResult> FindByIdAsync(Guid Id)
        {
            var result = await _repository.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Pedido>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Pedido entity, string[]? businessRules = null)
        {
            var lstWarnings = new List<string>();

            if (businessRules != null)
                lstWarnings.AddRange(businessRules);

            entity.IdPedido = entity.IdPedido.Equals(default) ? Guid.NewGuid() : entity.IdPedido;

            if (!await _dispositivoRepository.Any(x => ((Dispositivo)x).IdDispositivo.Equals(entity.IdDispositivo)))
                lstWarnings.Add(BusinessMessages.NotFoundInError<Dispositivo>(entity.IdDispositivo));

            if (!await _clienteRepository.Any(x => ((Cliente)x).IdCliente.Equals(entity.IdCliente)))
                lstWarnings.Add(BusinessMessages.NotFoundInError<Cliente>(entity.IdDispositivo));

            entity.DataStatusPedido = entity.Data = DateTime.Now;
            entity.Status = enmPedidoStatus.RECEBIDO.ToString();

            entity.DataStatusPagamento = DateTime.Now;
            entity.Status = enmPedidoStatusPagamento.PENDENTE.ToString();

            foreach (var itemPedido in entity.PedidoItems)
            {
                itemPedido.IdPedido = entity.IdPedido;
                itemPedido.IdPedidoItem = itemPedido.IdPedidoItem.Equals(default) ? Guid.NewGuid() : itemPedido.IdPedidoItem;
                itemPedido.Data = DateTime.Now;
                if (!await _produtoRepository.Any(x => ((Produto)x).IdProduto.Equals(itemPedido.IdProduto)))
                    lstWarnings.Add(BusinessMessages.NotFoundInError<Produto>(entity.IdDispositivo));
            }

            await _repository.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido recebido."
            });

            return await base.InsertAsync(entity, lstWarnings.ToArray());
        }

        /// <summary>
        /// Atualiza o objeto e suas dependências.
        /// </summary>
        public async override Task<ModelResult> UpdateAsync(Pedido entity, string[]? businessRules = null)
        {
            var dbEntity = await _repository.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == entity.IdPedido);

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

            await _repository.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Pedido em preparação.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> IniciarPreparacaoAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.EM_PREPARACAO.ToString();

            var transacao = _repository.BeginTransaction();
            _notificacaoRepository.UseTransaction(transacao);

            await _repository.UpdateAsync(entity);
            await _repository.CommitAsync();

            await _notificacaoRepository.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido em preparação."
            }); ;
            await _notificacaoRepository.CommitAsync();

            await transacao.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Pedido pronto.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> FinalizarPreparacaoAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.PRONTO.ToString();

            var transacao = _repository.BeginTransaction();
            _notificacaoRepository.UseTransaction(transacao);

            await _repository.UpdateAsync(entity);
            await _repository.CommitAsync();

            await _notificacaoRepository.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido pronto."
            });
            await _notificacaoRepository.CommitAsync();

            await transacao.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Pedido finalizado.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> FinalizarAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.FINALIZADO.ToString();

            await _repository.UpdateAsync(entity);
            await _repository.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        public async ValueTask<PagingQueryResult<Pedido>> GetListaAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Desc";
            return await _repository.GetItemsAsync(filter, x => x.Status != enmPedidoStatus.FINALIZADO.ToString(), o => o.Data);
        }


        /// <summary>
        /// Consulta o pagamento de um pedido.
        /// </summary>
        public async Task<ModelResult> ConsultarPagamentoAsync(Guid id)
        {
            var result = await _repository.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Pedido>();

            return ModelResultFactory.SucessResult(result.StatusPagamento);
        }

        /// <summary>
        ///  Webhook para notificação de pagamento.
        /// </summary>
        public async Task<ModelResult> WebhookPagamento(WebhookPagamento webhook, string[]? businessRules)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _repository.FindByIdAsync(webhook.IdPedido);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.DataStatusPagamento = DateTime.Now;
            entity.Status = webhook.StatusPagamento;

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            await _repository.UpdateAsync(entity);
            await _repository.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);
        }
    }
}
