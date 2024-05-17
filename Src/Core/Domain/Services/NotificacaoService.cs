using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class NotificacaoService : BaseService<Notificacao>
    {
        public NotificacaoService(IRepository<Notificacao> repository, IValidator<Notificacao> validator)
            : base(repository, validator)
        {
        }


        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Notificacao entity, string[]? businessRules = null)
        {
            entity.IdNotificacao = entity.IdNotificacao.Equals(default) ? Guid.NewGuid() : entity.IdNotificacao;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
