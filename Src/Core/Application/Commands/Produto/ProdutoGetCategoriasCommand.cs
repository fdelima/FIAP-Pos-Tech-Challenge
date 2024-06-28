using FIAP.Pos.Tech.Challenge.Domain;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.Commands.Produto
{
    internal class ProdutoGetCategoriasCommand : IRequest<PagingQueryResult<KeyValuePair<short, string>>>
    {
    }
}