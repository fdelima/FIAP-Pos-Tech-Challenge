using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido
{
    internal class PedidoGetItemsHandler : IRequestHandler<PedidoGetItemsCommand, PagingQueryResult<Domain.Entities.Pedido>>
    {
        private readonly IPedidoService _service;

        public PedidoGetItemsHandler(IPedidoService service)
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
