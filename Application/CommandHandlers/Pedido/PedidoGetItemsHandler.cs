using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido
{
    public class PedidoGetItemsHandler : IRequestHandler<PedidoGetItemsCommand, PagingQueryResult<Domain.Entities.Pedido>>
    {
        private readonly IService<Domain.Entities.Pedido> _service;

        public PedidoGetItemsHandler(IService<Domain.Entities.Pedido> service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Pedido>> Handle(PedidoGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
