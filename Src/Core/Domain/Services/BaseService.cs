using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    /// <summary>
    /// Métodos comuns dos serviços
    /// </summary>
    public class BaseService<TEntity> : IService<TEntity> where TEntity : class, IDomainEntity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IValidator<TEntity> _validator;

        public BaseService(IRepository<TEntity> repository, IValidator<TEntity> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        /// <summary>
        /// Inicia uma transação no banco de dados.
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTransaction()
          => _repository.BeginTransaction();

        /// <summary>
        /// Adiciona a transação ao contexto do banco de dados.
        /// </summary>
        /// <param name="transaction"></param>
        public void UseTransaction(IDbContextTransaction transaction)
          => _repository.UseTransaction(transaction);

        /// <summary>
        /// Valida o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public async Task<ModelResult> ValidateAsync(TEntity entity)
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
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public virtual async Task<ModelResult> InsertAsync(TEntity entity, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = await ValidateAsync(entity);

            Expression<Func<IDomainEntity, bool>> duplicatedExpression = entity.InsertDuplicatedRule();

            if (ValidatorResult.IsValid)
            {
                if (duplicatedExpression != null)
                {
                    bool duplicado = await _repository.Any(duplicatedExpression);

                    if (duplicado)
                        ValidatorResult.Add(ModelResultFactory.DuplicatedResult<TEntity>());
                }

                if (businessRules != null)
                    ValidatorResult.AddError(businessRules);

                if (!ValidatorResult.IsValid)
                    return ValidatorResult;

                await _repository.InsertAsync(entity);
                await _repository.CommitAsync();
                return ModelResultFactory.InsertSucessResult<TEntity>(entity);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Atualiza o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> UpdateAsync(TEntity entity, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = await ValidateAsync(entity);

            Expression<Func<IDomainEntity, bool>> duplicatedExpression = entity.AlterDuplicatedRule();

            if (ValidatorResult.IsValid)
            {
                if (duplicatedExpression != null)
                {
                    bool duplicado = await _repository.Any(duplicatedExpression);

                    if (duplicado)
                        ValidatorResult.Add(ModelResultFactory.DuplicatedResult<TEntity>());
                }

                if (businessRules != null)
                    ValidatorResult.AddError(businessRules);

                if (!ValidatorResult.IsValid)
                    return ValidatorResult;

                await _repository.UpdateAsync(entity);
                await _repository.CommitAsync();
                return ModelResultFactory.UpdateSucessResult<TEntity>(entity);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Atualiza o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> UpdateAsync(TEntity oldEntity, TEntity NewEntity, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = await ValidateAsync(NewEntity);

            Expression<Func<IDomainEntity, bool>> duplicatedExpression = NewEntity.AlterDuplicatedRule();

            if (ValidatorResult.IsValid)
            {
                if (duplicatedExpression != null)
                {
                    bool duplicado = await _repository.Any(duplicatedExpression);

                    if (duplicado)
                        ValidatorResult.Add(ModelResultFactory.DuplicatedResult<TEntity>());
                }

                if (businessRules != null)
                    ValidatorResult.AddError(businessRules);

                if (!ValidatorResult.IsValid)
                    return ValidatorResult;

                await _repository.UpdateAsync(oldEntity, NewEntity);
                await _repository.CommitAsync();
                return ModelResultFactory.UpdateSucessResult<TEntity>(NewEntity);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Deleta o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> DeleteAsync(Guid Id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            TEntity? entity = await _repository.FindByIdAsync(Id);
            if (entity == null) ValidatorResult.Add(ModelResultFactory.NotFoundResult<TEntity>());

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            try
            {
                await _repository.DeleteAsync(Id);
                await _repository.CommitAsync();
                return ModelResultFactory.DeleteSucessResult<TEntity>();
            }
            catch (Exception ex)
            {
                return ModelResultFactory.DeleteFailResult<TEntity>(ex.Message);
            }

        }

        /// <summary>
        /// Retorna o objeto no bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        public virtual async Task<ModelResult> FindByIdAsync(Guid Id)
        {
            TEntity? result = await _repository.FindByIdAsync(Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<TEntity>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Retorna os objetos do bd
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<TEntity, object>> sortProp)
            => await _repository.GetItemsAsync(filter, sortProp);


        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> sortProp)
            => await _repository.GetItemsAsync(filter, expression, sortProp);

    }
}
