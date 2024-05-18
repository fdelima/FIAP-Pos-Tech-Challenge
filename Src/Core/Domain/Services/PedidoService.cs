using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Messages;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class PedidoService : BaseService<Pedido>, IPedidoService
    {
        protected readonly IRepository<Notificacao> _notificacaoRepository;
        protected readonly IRepository<Dispositivo> _dispositivoRepository;
        protected readonly IRepository<Cliente> _clienteRepository;
        protected readonly IRepository<Produto> _produtoRepository;

        public PedidoService(IRepository<Pedido> repository,
            IValidator<Pedido> validator,
            IRepository<Notificacao> NotificacaoRepository,
            IRepository<Dispositivo> DispositivoRepository,
            IRepository<Cliente> ClienteRepository,
            IRepository<Produto> ProdutoRepository)
            : base(repository, validator)
        {
            _notificacaoRepository = NotificacaoRepository;
            _dispositivoRepository = DispositivoRepository;
            _clienteRepository = ClienteRepository;
            _produtoRepository = ProdutoRepository;
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
        /// Pedido em preparação.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> IniciarPreparacaoAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            var entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return ModelResultFactory.NotFoundResult<Pedido>();

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
                return ModelResultFactory.NotFoundResult<Pedido>();

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
                return ModelResultFactory.NotFoundResult<Pedido>();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.FINALIZADO.ToString();

            await _repository.UpdateAsync(entity);
            await _repository.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }
    }
}
