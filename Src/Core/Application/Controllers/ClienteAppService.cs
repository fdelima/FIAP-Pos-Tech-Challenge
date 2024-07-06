using FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Application.Controllers
{
    /// <summary>
    /// Métodos do serviço da plicação
    /// </summary>
    public class ClienteAppService : IController<Domain.Entities.Cliente>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Domain.Entities.Cliente> _validator;

        public ClienteAppService(IMediator mediator, IValidator<Domain.Entities.Cliente> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public async Task<ModelResult> ValidateAsync(Domain.Entities.Cliente entity)
        {
            ModelResult ValidatorResult = new ModelResult(entity);

            FluentValidation.Results.ValidationResult validations = _validator.Validate(entity);
            if (!validations.IsValid)
            {
                ValidatorResult.AddValidations(validations);
                return ValidatorResult;
            }

            return await Task.FromResult(ValidatorResult);
        }

        /// <summary>
        /// Envia o objeto para inserção ao domínio
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> PostAsync(Domain.Entities.Cliente entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Cliente");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ClientePostCommand command = new(entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia o objeto para atualização ao domínio
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult> PutAsync(Guid id, Domain.Entities.Cliente entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Cliente");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ClientePutCommand command = new(id, entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia o objeto para deleção ao domínio
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> DeleteAsync(Guid id)
        {
            ClienteDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna o objeto no bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> FindByIdAsync(Guid id)
        {
            ClienteFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna os objetos do bd
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Cliente>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Cliente, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ClienteGetItemsCommand command = new(filter, sortProp);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Cliente>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Cliente, bool>> expression, Expression<Func<Domain.Entities.Cliente, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ClienteGetItemsCommand command = new(filter, expression, sortProp);
            return await _mediator.Send(command);
        }

    }
}
