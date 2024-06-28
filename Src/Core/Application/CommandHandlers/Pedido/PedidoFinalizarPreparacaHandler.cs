using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido
{
    internal class PedidoFinalizarPreparacaHandler : IRequestHandler<PedidoFinalizarPreparacaCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoFinalizarPreparacaHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoFinalizarPreparacaCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FinalizarPreparacaoAsync(command.Id, command.BusinessRules);
        }
    }
}
