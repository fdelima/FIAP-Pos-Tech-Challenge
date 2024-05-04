using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido
{
    public class PedidoDeleteHandler : IRequestHandler<PedidoDeleteCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Pedido> _service;

        public PedidoDeleteHandler(IService<Domain.Entities.Pedido> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
