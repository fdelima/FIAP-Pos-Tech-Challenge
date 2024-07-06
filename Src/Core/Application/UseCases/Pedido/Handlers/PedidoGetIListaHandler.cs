using FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Handlers
{
    internal class PedidoGetIListaHandler : IRequestHandler<PedidoGetListaCommand, PagingQueryResult<Domain.Entities.Pedido>>
    {
        private readonly IPedidoService _service;

        public PedidoGetIListaHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Pedido>> Handle(PedidoGetListaCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.GetListaAsync(command.Filter);
        }
    }
}
