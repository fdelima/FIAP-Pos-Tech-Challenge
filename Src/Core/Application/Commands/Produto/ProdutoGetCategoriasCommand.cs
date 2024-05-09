using FIAP.Pos.Tech.Challenge.Domain;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.Commands.Produto
{
    public class ProdutoGetCategoriasCommand : IRequest<PagingQueryResult<KeyValuePair<short, string>>>
    {
    }
}