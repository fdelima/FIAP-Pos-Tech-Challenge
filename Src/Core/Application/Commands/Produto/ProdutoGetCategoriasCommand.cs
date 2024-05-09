using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.Commands.Produto
{
    public class ProdutoGetCategoriasCommand : IRequest<PagingQueryResult<KeyValuePair<short, string>>>
    {
    }
}