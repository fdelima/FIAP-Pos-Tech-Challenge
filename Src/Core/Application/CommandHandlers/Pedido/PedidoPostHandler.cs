using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido
{
    internal class PedidoPostHandler : IRequestHandler<PedidoPostCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoPostHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoPostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
