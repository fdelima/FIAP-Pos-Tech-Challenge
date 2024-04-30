using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Application.Commands.Client
{
    public class ClientGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Client>>
    {
        public ClientGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.Client, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public ClientGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.Client, bool>> expression, Expression<Func<Domain.Entities.Client, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.Client, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.Client, object>> SortProp { get; }
    }
}