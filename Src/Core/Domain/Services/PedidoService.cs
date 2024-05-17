using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class PedidoService : BaseService<Pedido>, IPedidoService
    {
        protected readonly IRepository<Notificacao> _notificacaoRepository;

        public PedidoService(IRepository<Pedido> repository, IValidator<Pedido> validator,
            IRepository<Notificacao> NotificacaoRepository)
            : base(repository, validator)
        {
            _notificacaoRepository = NotificacaoRepository;
        }

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Pedido entity, string[]? businessRules = null)
        {
            entity.IdPedido = entity.IdPedido.Equals(default) ? Guid.NewGuid() : entity.IdPedido;
            return await base.InsertAsync(entity, businessRules);
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

            entity.Status = enmPedidoStatus.EM_PREPARACAO;

            var transacao = _repository.BeginTransaction();
            _notificacaoRepository.UseTransaction(transacao);

            await _repository.InsertAsync(entity);
            await _notificacaoRepository.InsertAsync(new Notificacao
            {
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido em preparação."
            });

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

            entity.Status = enmPedidoStatus.PRONTO;

            var transacao = _repository.BeginTransaction();
            _notificacaoRepository.UseTransaction(transacao);

            await _repository.InsertAsync(entity);
            await _notificacaoRepository.InsertAsync(new Notificacao
            {
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido pronto."
            });

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

            entity.Status = enmPedidoStatus.FINALIZADO;

            await _repository.InsertAsync(entity);
            await _repository.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }
    }
}
