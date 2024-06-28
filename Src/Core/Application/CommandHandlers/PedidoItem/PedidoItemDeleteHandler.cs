using FIAP.Pos.Tech.Challenge.Application.Commands.PedidoItem;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.PedidoItem
{
    internal class PedidoItemDeleteHandler : IRequestHandler<PedidoItemDeleteCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.PedidoItem> _service;

        public PedidoItemDeleteHandler(IService<Domain.Entities.PedidoItem> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoItemDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
