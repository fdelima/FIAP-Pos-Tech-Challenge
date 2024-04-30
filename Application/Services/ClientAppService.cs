using FIAP.Pos.Tech.Challenge.Application.Commands.Client;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Messages;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Application.Services
{
    /// <summary>
    /// Métodos do serviço da plicação
    /// </summary>
    public class ClientAppService : IAppService<Domain.Entities.Client>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Domain.Entities.Client> _validator;

        public ClientAppService(IMediator mediator, IValidator<Domain.Entities.Client> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public async Task<ModelResult> ValidateAsync(Domain.Entities.Client entity)
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
        public virtual async Task<ModelResult> PostAsync(Domain.Entities.Client entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o client");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ClientPostCommand command = new(entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia o objeto para atualização ao domínio
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult> PutAsync(Guid id, Domain.Entities.Client entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o client");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ClientPutCommand command = new(id, entity);
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
            ClientDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna o objeto no bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> FindByIdAsync(Guid id)
        {
            ClientFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna os objetos do bd
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Client>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Client, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ClientGetItemsCommand command = new(filter, sortProp);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Client>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Client, bool>> expression, Expression<Func<Domain.Entities.Client, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ClientGetItemsCommand command = new(filter, expression, sortProp);
            return await _mediator.Send(command);
        }

    }
}
