﻿using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;

namespace FIAP.Pos.Tech.Challenge.Domain.Interfaces
{
    public interface IProdutoService : IService<Produto>
    {
        /// <summary>
        /// Retorna as categorias dos produtos
        /// </summary>
        public Task<PagingQueryResult<KeyValuePair<short, string>>> GetCategoriasAsync();
    }
}
