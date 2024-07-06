using FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands;
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
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Domain.Entities.Produto> _validator;

        public ProdutoAppService(IMediator mediator, IValidator<Domain.Entities.Produto> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public async Task<ModelResult> ValidateAsync(Domain.Entities.Produto entity)
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
        public virtual async Task<ModelResult> PostAsync(Domain.Entities.Produto entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Produto");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ProdutoPostCommand command = new(entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia o objeto para atualização ao domínio
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult> PutAsync(Guid id, Domain.Entities.Produto entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Produto");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ProdutoPutCommand command = new(id, entity);
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
            ProdutoDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna o objeto no bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> FindByIdAsync(Guid id)
        {
            ProdutoFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna os objetos do bd
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Produto>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Produto, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ProdutoGetItemsCommand command = new(filter, sortProp);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Produto>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Produto, bool>> expression, Expression<Func<Domain.Entities.Produto, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ProdutoGetItemsCommand command = new(filter, expression, sortProp);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna as categorias dos produtos
        /// </summary>
        public async Task<PagingQueryResult<KeyValuePair<short, string>>> GetCategoriasAsync()
        {
            ProdutoGetCategoriasCommand command = new();
            return await _mediator.Send(command);

        }
    }
}
