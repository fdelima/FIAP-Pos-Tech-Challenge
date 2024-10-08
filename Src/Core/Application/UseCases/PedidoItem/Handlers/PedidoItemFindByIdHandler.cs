﻿using FIAP.Pos.Tech.Challenge.Application.UseCases.PedidoItem.Commands;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.PedidoItem.Handlers
{
    internal class PedidoItemFindByIdHandler : IRequestHandler<PedidoItemFindByIdCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.PedidoItem> _service;

        public PedidoItemFindByIdHandler(IService<Domain.Entities.PedidoItem> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoItemFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
