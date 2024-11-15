﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Commands
{
    public class PedidoPostCommand : IRequest<ModelResult>
    {
        public PedidoPostCommand(Domain.Entities.Pedido entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Pedido Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}