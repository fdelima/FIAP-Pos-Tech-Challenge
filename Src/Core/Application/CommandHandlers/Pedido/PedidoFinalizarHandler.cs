﻿using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido
{
    public class PedidoFinalizarHandler : IRequestHandler<PedidoFinalizarCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoFinalizarHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoFinalizarCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FinalizarAsync(command.Id, command.BusinessRules);
        }
    }
}
