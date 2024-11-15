﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.PedidoItem.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.PedidoItem.Handlers
{
    internal class PedidoItemPostHandler : IRequestHandler<PedidoItemPostCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.PedidoItem> _service;

        public PedidoItemPostHandler(IService<Domain.Entities.PedidoItem> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoItemPostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
