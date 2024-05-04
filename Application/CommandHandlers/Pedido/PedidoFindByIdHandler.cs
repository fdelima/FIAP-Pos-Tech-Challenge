﻿using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido
{
    public class PedidoFindByIdHandler : IRequestHandler<PedidoFindByIdCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Pedido> _service;

        public PedidoFindByIdHandler(IService<Domain.Entities.Pedido> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
