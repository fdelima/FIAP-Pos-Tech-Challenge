using FIAP.Pos.Tech.Challenge.Application.Commands.Produto;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Produto
{
    public class ProdutoGetItemsHandler : IRequestHandler<ProdutoGetItemsCommand, PagingQueryResult<Domain.Entities.Produto>>
    {
        private readonly IService<Domain.Entities.Produto> _service;

        public ProdutoGetItemsHandler(IService<Domain.Entities.Produto> service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Produto>> Handle(ProdutoGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
