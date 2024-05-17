using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class ClienteService : BaseService<Cliente>
    {
        public ClienteService(IRepository<Cliente> repository, IValidator<Cliente> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Cliente entity, string[]? businessRules = null)
        {
            entity.IdCliente = entity.IdCliente.Equals(default) ? Guid.NewGuid() : entity.IdCliente;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
