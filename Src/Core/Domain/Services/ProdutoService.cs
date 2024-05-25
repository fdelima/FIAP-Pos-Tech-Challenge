﻿using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        public ProdutoService(IRepository<Produto> repository, IValidator<Produto> validator)
            : base(repository, validator) { }

        /// <summary>
        /// Carrega o produtos e suas imagens.
        /// </summary>
        public async override Task<ModelResult> FindByIdAsync(Guid Id)
        {
            var result = await _repository.FirstOrDefaultWithIncludeAsync(x => x.ProdutoImagens, x => x.IdProduto == Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Produto>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Atualiza o objeto e suas dependências.
        /// </summary>
        public async override Task<ModelResult> UpdateAsync(Produto entity, string[]? businessRules = null)
        {
            var dbEntity = await _repository.FirstOrDefaultWithIncludeAsync(x => x.ProdutoImagens, x => x.IdProduto == entity.IdProduto);

            if (dbEntity == null)
                return ModelResultFactory.NotFoundResult<Produto>();

            for (int i = 0; i < dbEntity.ProdutoImagens.Count; i++)
            {
                var item = dbEntity.ProdutoImagens.ElementAt(i);
                if (!entity.ProdutoImagens.Any(x => x.IdProdutoImagem.Equals(item.IdProdutoImagem)))
                    dbEntity.ProdutoImagens.Remove(dbEntity.ProdutoImagens.First(x => x.IdProdutoImagem.Equals(item.IdProdutoImagem)));
            }

            for (int i = 0; i < entity.ProdutoImagens.Count; i++)
            {
                var item = entity.ProdutoImagens.ElementAt(i);
                if (!dbEntity.ProdutoImagens.Any(x => x.IdProdutoImagem.Equals(item.IdProdutoImagem)))
                {
                    item.IdProdutoImagem = item.IdProdutoImagem.Equals(default) ? Guid.NewGuid() : item.IdProdutoImagem;
                    dbEntity.ProdutoImagens.Add(item);
                }
            }

            await _repository.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Retorna as categorias dos produtos
        /// </summary>
        public Task<PagingQueryResult<KeyValuePair<short, string>>> GetCategoriasAsync()
        {
            List<KeyValuePair<short, string>> content = new List<KeyValuePair<short, string>>();

            foreach (enmProdutoCategoria value in Enum.GetValues<enmProdutoCategoria>())
                content.Add(new KeyValuePair<short, string>((short)value, value.ToString()));

            return Task.FromResult(new PagingQueryResult<KeyValuePair<short, string>>(content));

        }

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Produto entity, string[]? businessRules = null)
        {
            entity.IdProduto = entity.IdProduto.Equals(default) ? Guid.NewGuid() : entity.IdProduto;

            foreach (var item in entity.ProdutoImagens)
                item.IdProdutoImagem = item.IdProdutoImagem.Equals(default) ? Guid.NewGuid() : item.IdProdutoImagem;

            return await base.InsertAsync(entity, businessRules);
        }
    }
}
