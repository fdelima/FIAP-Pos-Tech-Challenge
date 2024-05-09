using FIAP.Pos.Tech.Challenge.Domain.Entities;

namespace FIAP.Pos.Tech.Challenge.Domain.Interfaces
{
    public interface IProdutoAppService : IAppService<Produto>
    {
        /// <summary>
        /// Retorna as categorias dos produtos
        /// </summary>
        public Task<PagingQueryResult<KeyValuePair<short, string>>> GetCategoriasAsync();
    }
}
