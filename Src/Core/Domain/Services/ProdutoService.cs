using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;
using System.Windows.Markup;

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
            var content = new List<KeyValuePair<short, string>>();

            foreach (var value in Enum.GetValues<enmProdutoCategoria>())
                content.Add(new KeyValuePair<short, string>((short)value, value.ToString()));

            return Task.FromResult(new PagingQueryResult<KeyValuePair<short, string>>(content));

        }
    }
}
