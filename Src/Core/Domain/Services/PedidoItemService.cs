using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class PedidoItemService : BaseService<PedidoItem>
    {
        public PedidoItemService(IRepository<PedidoItem> repository, IValidator<PedidoItem> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(PedidoItem entity, string[]? businessRules = null)
        {
            entity.IdPedidoItem = entity.IdPedidoItem.Equals(default) ? Guid.NewGuid() : entity.IdPedidoItem;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
