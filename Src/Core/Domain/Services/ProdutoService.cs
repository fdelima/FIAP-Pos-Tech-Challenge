using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        public ProdutoService(IRepository<Produto> repository, IValidator<Produto> validator)
            : base(repository, validator)
        {
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
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
