using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class DispositivoService : BaseService<Dispositivo>
    {
        public DispositivoService(IGateways<Dispositivo> repository, IValidator<Dispositivo> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Dispositivo entity, string[]? businessRules = null)
        {
            entity.IdDispositivo = entity.IdDispositivo.Equals(default) ? Guid.NewGuid() : entity.IdDispositivo;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
